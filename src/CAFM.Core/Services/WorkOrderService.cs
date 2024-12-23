using CAFM.Database.Entities;
using CAFM.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data;
using System.IO;
using Microsoft.EntityFrameworkCore;
using CAFM.Core.Resources;

namespace CAFM.Core.Services
{
    public class WorkOrderService
    {
        private readonly CAFMDbContext _dbContext;
        private readonly ErrorLogService _errorLogService;

        //Using Portable Object (PO)  Files will be a better,
        //using the normal text, then add a translated file is better than
        //using variables of the text, and it should take less steps 
        //Human-Readable: Easy for translators to edit directly.
        //Standardized: Compatible with many frameworks and platforms.
        //Efficient: Enables easy reuse of translations across projects.
        private readonly IStringLocalizer<Messages> _localizer;
        public WorkOrderService(CAFMDbContext dbContext, ErrorLogService errorLogService, IStringLocalizer<Messages> localizer)
        {
            _dbContext = dbContext;
            _errorLogService = errorLogService;
            _localizer = localizer;
        }

        public async Task<string> SaveWorkOrderAsync(WorkOrder workOrder)
        {
            var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                if (workOrder.StartDate == null)
                    throw new Exception(_localizer["StartDateRequired"]);

                if (workOrder.TaskTypeId != 134 && workOrder.StartDate > workOrder.DueDate)
                    throw new Exception(_localizer["DueDateValidationError"]);

                workOrder.InternalNumber = await GenerateInternalNumberAsync(workOrder.CompanyId, workOrder.LocationId);

                _dbContext.WorkOrders.Add(workOrder);
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();

                return _localizer["SuccessMessage", workOrder.InternalNumber];
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                await _errorLogService.LogErrorAsync(
                    workOrder.CreatedBy.ToString(),
                    workOrder.CompanyId,
                    null,
                    nameof(SaveWorkOrderAsync),
                    ex.Message,
                    1,
                    workOrder.ID.ToString()
                );

                throw;
            }
        }

        private async Task<long> GenerateInternalNumberAsync(int companyId, long locationId)
        {
            return await Task.FromResult(DateTime.UtcNow.Ticks);
        }

        public async Task<WorkOrder> GetWorkOrderByIdAsync(long id)
        {
            try
            {
                var workOrder = await _dbContext.WorkOrders
                    .Include(w => w.WorkOrderDetails) // Include related details if applicable
                    .FirstOrDefaultAsync(w => w.ID == id);

                if (workOrder == null)
                    throw new KeyNotFoundException("Work order not found.");

                return workOrder;
            }
            catch (Exception ex)
            {
                await _errorLogService.LogErrorAsync(
                    userName: "System", // Use a system-level user or pass from context
                    companyId: 0, // Replace with appropriate company ID if available
                    localHost: null,
                    errSource: nameof(GetWorkOrderByIdAsync),
                    errMsg: ex.Message,
                    errFrom: 1,
                    serial: id.ToString()
                );

                throw;
            }
        }

        public async Task UpdateWorkOrderStatusAsync(long id, string status)
        {
            try
            {
                var workOrder = await _dbContext.WorkOrders.FirstOrDefaultAsync(w => w.ID == id);

                if (workOrder == null)
                    throw new KeyNotFoundException("Work order not found.");

                // Assuming TaskStatusId is mapped to status
                int taskStatusId = MapStatusToId(status);

                workOrder.TaskStatusId = taskStatusId;
                workOrder.ModifiedDate = DateTime.Now;

                _dbContext.WorkOrders.Update(workOrder);
                await _dbContext.SaveChangesAsync();
            }
            catch (KeyNotFoundException)
            {
                throw; // Let the controller handle not-found exceptions
            }
            catch (Exception ex)
            {
                await _errorLogService.LogErrorAsync(
                    userName: "System", // Use a system-level user or pass from context
                    companyId: 0, // Replace with appropriate company ID if available
                    localHost: null,
                    errSource: nameof(UpdateWorkOrderStatusAsync),
                    errMsg: ex.Message,
                    errFrom: 1,
                    serial: id.ToString()
                );

                throw;
            }
        }

        private int MapStatusToId(string status)
        {
            // Example mapping, replace with actual logic or database lookup
            return status.ToLower() switch
            {
                "new" => 1,
                "in progress" => 2,
                "completed" => 3,
                "cancelled" => 4,
                _ => throw new ArgumentException("Invalid status.")
            };
        }
    }

}
