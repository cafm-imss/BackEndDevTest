using CAFM.Core.Hubs;
using CAFM.Core.Interfaces;
using CAFM.Database.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

public class WorkOrderService : IWorkOrderService
{
    private readonly IHubContext<WorkOrderHub> _hubContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<WorkOrderService> _logger;

    public WorkOrderService(IUnitOfWork unitOfWork, IHubContext<WorkOrderHub> hubContext, ILogger<WorkOrderService> logger)
    {
        _unitOfWork = unitOfWork;
        _hubContext = hubContext;
        _logger = logger;
    }

    public async Task<long> SaveWorkOrderAsync(WorkOrder workOrder)
    {
        try
        {
            using var transaction = await _unitOfWork.GetDbContext().Database.BeginTransactionAsync();

            // Validation
            if (string.IsNullOrEmpty(workOrder.TaskName))
                throw new ArgumentException("Task Name is required.");

            workOrder.InternalNumber = await GenerateInternalNumberAsync();

            _unitOfWork.WorkOrderRepository.Add(workOrder);
            await _unitOfWork.SaveChangesAsync();

            var groupName = $"Company_{workOrder.CompanyId}_Location_{workOrder.LocationId}";
            await _hubContext.Clients.Group(groupName).SendAsync("ReceiveWorkOrderUpdate", workOrder.Id, workOrder.TaskStatus.StatusName);

            await transaction.CommitAsync();
            return workOrder.Id;
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Argument error while saving work order.");
            throw; // Rethrow to be caught by controller
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while saving the work order.");
            throw new Exception("An error occurred while processing your request.", ex);
        }
    }

    public async Task<long> GenerateInternalNumberAsync()
    {
        // Fetch the highest existing internal number
        var lastInternalNumber = await _unitOfWork.WorkOrderRepository
            .FindAllAsync(w => w.InternalNumber > 0)
            .ContinueWith(task => task.Result.Max(w => (long?)w.InternalNumber) ?? 0);

        return lastInternalNumber + 1;
    }

    public async Task<WorkOrder?> GetWorkOrderByIdAsync(long workOrderId)
    {
        try
        {
            var workOrder = await _unitOfWork.WorkOrderRepository.FindAsync(a => a.Id == workOrderId);
            if (workOrder == null)
                throw new KeyNotFoundException($"WorkOrder with ID {workOrderId} not found.");

            // Fetch related details, wrapped in individual try-catch blocks for each related entity (to handle specific errors)

            // Handle WorkOrderDetails fetching
            try
            {
                workOrder.WorkOrderDetails = (await _unitOfWork.WorkOrderDetailRepository.FindAllAsync(w => w.WorkOrderId == workOrderId)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching WorkOrder details.");
                throw;
            }

            // Handle Asset fetching
            try
            {
                if (workOrder.AssetId.HasValue)
                    workOrder.Asset = await _unitOfWork.AssetRepository.GetByIdAsync(workOrder.AssetId.Value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Asset.");
                throw;
            }

            // Other related entities fetch can be similarly wrapped

            return workOrder;
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogError(ex, "WorkOrder not found.");
            throw; // Rethrow to be caught by controller
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching the WorkOrder.");
            throw new Exception("An error occurred while processing your request.", ex);
        }
    }

    public async Task<bool> UpdateWorkOrderStatusAsync(long id, int statusUpdate)
    {
        try
        {
            var workOrder = await _unitOfWork.WorkOrderRepository.FindAsync(w => w.Id == id);
            if (workOrder == null)
            {
                _logger.LogWarning($"WorkOrder with ID {id} not found.");
                return false;
            }

            var status = await _unitOfWork.TaskStatueRepository.FindAsync(s => s.Id == statusUpdate);
            if (status == null)
            {
                _logger.LogWarning($"Status {statusUpdate} not found.");
                return false;
            }

            workOrder.TaskStatusId = status.Id;
            if (status.IsStart)
                workOrder.StartDate = workOrder.StartDate ?? DateTime.UtcNow;

            if (status.IsCompleted)
            {
                workOrder.CompletionDate = DateTime.UtcNow;
                workOrder.CompletionRatio = 100;
            }

            _unitOfWork.WorkOrderRepository.Update(workOrder);
            await _unitOfWork.SaveChangesAsync();

            var groupName = $"Company_{workOrder.CompanyId}_Location_{workOrder.LocationId}";
            await _hubContext.Clients.Group(groupName).SendAsync("ReceiveWorkOrderUpdate", workOrder.Id, workOrder.TaskStatus.StatusName);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating WorkOrder status.");
            throw new Exception("An error occurred while processing your request.", ex);
        }
    }
}
