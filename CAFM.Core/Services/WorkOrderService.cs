using CAFM.Core.Interfaces;
using CAFM.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CAFM.Core.Services
{
    public class WorkOrderService : IWorkOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public WorkOrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<long> SaveWorkOrderAsync(WorkOrder workOrder)
        {
            using var transaction = await _unitOfWork.GetDbContext().Database.BeginTransactionAsync();

            try
            {
                // Validation Rules
                if (string.IsNullOrEmpty(workOrder.TaskName))
                    throw new ArgumentException("Task Name is required.");

                // Internal Number Generation
                workOrder.InternalNumber = await GenerateInternalNumberAsync();

                // Add Work Order
                _unitOfWork.WorkOrderRepository.Add(workOrder);
                await _unitOfWork.SaveChangesAsync();

                await transaction.CommitAsync();
                return workOrder.Id;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
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
                // Fetch the work order
                var workOrder = await _unitOfWork.WorkOrderRepository.FindAsync(a => a.Id == workOrderId);
                if (workOrder == null)
                {
                    throw new KeyNotFoundException($"WorkOrder with ID {workOrderId} not found.");
                }

                // Fetch related details
                try
                {
                    workOrder.WorkOrderDetails = (await _unitOfWork.WorkOrderDetailRepository.FindAllAsync(w => w.WorkOrderId == workOrderId)).ToList();
                }
                catch (Exception ex)
                {
                    // Handle WorkOrderDetails related exception
                    // Log the error or handle as needed
                    throw new Exception("An error occurred while fetching WorkOrder details.", ex);
                }

                try
                {
                    if (workOrder.AssetId.HasValue)
                    {
                        workOrder.Asset = await _unitOfWork.AssetRepository.GetByIdAsync(workOrder.AssetId.Value);
                    }
                }
                catch (Exception ex)
                {
                    // Handle Asset related exception
                    // Log the error or handle as needed
                    throw new Exception("An error occurred while fetching the related Asset.", ex);
                }

                try
                {
                    workOrder.TaskStatus =  _unitOfWork.TaskStatueRepository.Find(a=>a.Id == workOrder.TaskStatusId); 
                }
                catch (Exception ex)
                {
                    // Handle TaskStatus related exception
                    // Log the error or handle as needed
                    throw new Exception("An error occurred while fetching the related Task Status.", ex);
                }

                try
                {
                    workOrder.Priority = await _unitOfWork.TaskPriorityRepository.GetByIdAsync(workOrder.PriorityId);
                }
                catch (Exception ex)
                {
                    // Handle TaskPriority related exception
                    // Log the error or handle as needed
                    throw new Exception("An error occurred while fetching the related Task Priority.", ex);
                }

                return workOrder;
            }
            catch (Exception ex)
            {
                // Handle any other unexpected exceptions
                // Log the error or handle as needed
                throw new Exception("An error occurred while fetching the Work Order.", ex);
            }
        }


        public async Task<bool> UpdateWorkOrderStatusAsync(long id, int statusUpdate)
        {
            // Retrieve the work order by ID
            var workOrder = await _unitOfWork.WorkOrderRepository
                .FindAsync(w => w.Id == id);

            if (workOrder == null)
            {
                return false; // Work order not found
            }

            // Retrieve the status from the TaskStatue repository
            var status = await _unitOfWork.TaskStatueRepository
                .FindAsync(s => s.Id == statusUpdate);

            if (status == null)
            {
                return false; // Invalid status
            }

            // Update work order status
            workOrder.TaskStatusId = status.Id;

            // Update flags based on status logic
            if (status.IsStart)
            {
                workOrder.StartDate = workOrder.StartDate ?? System.DateTime.Now;
            }
            if (status.IsCompleted)
            {
                workOrder.CompletionDate = System.DateTime.Now;
                workOrder.CompletionRatio = 100;
            }

            // Save changes
            _unitOfWork.WorkOrderRepository.Update(workOrder);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
