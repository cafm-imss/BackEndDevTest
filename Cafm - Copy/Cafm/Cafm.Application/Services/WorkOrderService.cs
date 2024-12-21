using Cafm.Application.Hubs;
using Cafm.Application.Interfaces;
using Cafm.Common;
using Cafm.Common.WorkOrder;
using Cafm.Domain;
using Cafm.Framework.UnitOfWork;
using Cafm.Infrastructure.Data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafm.Application.Services
{
    public class WorkOrderService : IWorkOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CafmContext _context;
        private readonly IHubContext<WorkOrderHub> _hubContext;

        public WorkOrderService(IUnitOfWork unitOfWork, CafmContext context, IHubContext<WorkOrderHub> hubContext)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _hubContext = hubContext;
        }

        // Save a WorkOrder
        public async Task<BaseCommandResponse<bool>> SaveWorkOrderAsync(WorkOrderDto workOrderDto)
        {
            try
            {
                // Create the execution strategy
                var strategy = _context.Database.CreateExecutionStrategy();

                // Execute the entire operation as a retryable unit
                await strategy.ExecuteAsync(async () =>
                {
                    // Begin transaction using UnitOfWork
                    await _unitOfWork.BeginTransactionAsync();

                    // Validation Rules
                    if (workOrderDto.StartDate == null || workOrderDto.DueDate == null)
                    {
                        throw new ArgumentException("Start and Due dates are required.");
                    }

                    if (workOrderDto.StartDate > workOrderDto.DueDate)
                    {
                        throw new ArgumentException("Due date cannot be earlier than the start date.");
                    }

                    // Generate Internal Number for the WorkOrder
                    var internalNumber = await GenerateInternalNumber(workOrderDto.CompanyId, workOrderDto.LocationId);

                    // Create and populate WorkOrder entity
                    var workOrder = new WorkOrder
                    {
                        CompanyId = workOrderDto.CompanyId,
                        LocationId = workOrderDto.LocationId,
                        InternalNumber = internalNumber,
                        TaskName = workOrderDto.TaskName,
                        StartDate = workOrderDto.StartDate,
                        DueDate = workOrderDto.DueDate.Value,
                        PriorityId = workOrderDto.PriorityId,
                        CreatedDate = DateTime.UtcNow // Add timestamp for the creation date
                    };

                    // Get the repository and save the WorkOrder
                    var workOrderRepository = _unitOfWork.GetRepository<WorkOrder>();
                    await workOrderRepository.InsertAsync(workOrder);
                    await _unitOfWork.SaveChangesAsync();  // Save WorkOrder to the DB

                    // Broadcast work order creation notification
                    await _hubContext.Clients.Group($"{workOrderDto.CompanyId}_{workOrderDto.LocationId}")
                        .SendAsync("ReceiveWorkOrderUpdate", $"New work order created: {workOrder.InternalNumber}");


                    // Save the related WorkOrderDetails
                    foreach (var detail in workOrderDto.Details)
                    {
                        var workOrderDetail = new WorkOrderDetail
                        {
                            WorkOrderId = workOrder.Id,
                            Latitude = detail.Latitude,
                            Longitude = detail.Longitude
                        };

                        // Use repository for WorkOrderDetail
                        var workOrderDetailRepository = _unitOfWork.GetRepository<WorkOrderDetail>();
                        await workOrderDetailRepository.InsertAsync(workOrderDetail);
                    }

                    await _unitOfWork.SaveChangesAsync();  // Save WorkOrderDetails to the DB

                    // Commit transaction
                    await _unitOfWork.CommitTransactionAsync();
                });

                return new BaseCommandResponse<bool>
                {
                    IsSuccess = true,
                    Message = "Work order saved successfully."
                };
            }
            catch (Exception ex)
            {
                // Rollback transaction on failure
                await _unitOfWork.RollbackTransactionAsync();
                return new BaseCommandResponse<bool>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        private async Task<long> GenerateInternalNumber(int companyId, long locationId)
        {
            var lastInternalNumber = (await _unitOfWork.GetRepository<WorkOrder>().GetAllAsync(wo => wo.CompanyId == companyId && wo.LocationId == locationId))
                .Max(wo => (long?)wo.InternalNumber);

            // Increment the last internal number or set to 1 if no existing record
            return (lastInternalNumber ?? 0) + 1;
        }

        // Get a WorkOrder by its ID
        public async Task<BaseCommandResponse<WorkOrderDto>> GetWorkOrderByIdAsync(long id)
        {
            try
            {
                var workOrder = (await _unitOfWork.GetRepository<WorkOrder>().GetAllAsync(wo => wo.Id == id))
                    .Select(wo => new WorkOrderDto
                    {
                        Id = wo.Id,
                        CompanyId = wo.CompanyId,
                        LocationId = wo.LocationId,
                        InternalNumber = wo.InternalNumber,
                        TaskName = wo.TaskName,
                        StartDate = wo.StartDate,
                        DueDate = wo.DueDate,
                        PriorityId = wo.PriorityId,
                        CreatedDate = wo.CreatedDate,
                        Details = wo.WorkOrderDetails.Select(d => new WorkOrderDetailDto
                        {
                            Latitude = d.Latitude,
                            Longitude = d.Longitude
                        }).ToList()
                    })
                    .FirstOrDefault();

                if (workOrder == null)
                    return new BaseCommandResponse<WorkOrderDto>
                    {
                        IsSuccess = false,
                        Message = "Work order not found."
                    };

                return new BaseCommandResponse<WorkOrderDto>
                {
                    IsSuccess = true,
                    ResponseData = workOrder,
                    Message = "Work order retrieved successfully."
                };
            }
            catch (Exception ex)
            {
                return new BaseCommandResponse<WorkOrderDto>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        // Update WorkOrder Status
        public async Task<BaseCommandResponse<bool>> UpdateWorkOrderStatusAsync(long id, int newStatusId)
        {
            try
            {
                var workOrder = (await _unitOfWork.GetRepository<WorkOrder>().FindAsync(id));
                if (workOrder == null)
                    return new BaseCommandResponse<bool>
                    {
                        IsSuccess = false,
                        Message = "WorkOrder not found."
                    };

                workOrder.TaskStatusId = newStatusId;
                await _unitOfWork.SaveChangesAsync();

                return new BaseCommandResponse<bool>
                {
                    IsSuccess = true,
                    Message = "Work order status updated successfully."
                };
            }
            catch (Exception ex)
            {
                return new BaseCommandResponse<bool>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }


}
