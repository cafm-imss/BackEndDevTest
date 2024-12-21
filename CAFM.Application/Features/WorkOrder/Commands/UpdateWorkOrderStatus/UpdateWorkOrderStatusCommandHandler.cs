using CAFM.Application.Contracts.Repos;
using CAFM.Application.Hubs;
using CAFM.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace CAFM.Application.Features.WorkOrder.Commands.UpdateWorkOrderStatus
{
    internal class UpdateWorkOrderStatusCommandHandler : IRequestHandler<UpdateWorkOrderStatusCommand, ApiResponse<UpdateWorkOrderStatusCommandResponse>>
    {
        private readonly IWorkOrderRepo _workOrderRepo;
        private readonly ITaskStatusRepo _taskStatusRepo;
        private readonly IHubContext<WorkOrderHub> _workOrderHub;
        public UpdateWorkOrderStatusCommandHandler(IWorkOrderRepo workOrderRepo, IHubContext<WorkOrderHub> workOrderHub, ITaskStatusRepo taskStatusRepo)
        {
            _workOrderRepo = workOrderRepo;
            _workOrderHub = workOrderHub;
            _taskStatusRepo = taskStatusRepo;
        }

        public async Task<ApiResponse<UpdateWorkOrderStatusCommandResponse>> Handle(UpdateWorkOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var taskStatusExist = await _taskStatusRepo.IsTaskStatusExist(request.TaskStatusId);
            if (!taskStatusExist)
                return ApiResponse<UpdateWorkOrderStatusCommandResponse>.GetNotFoundApiResponse("Task Status not found");

            var workOrder = await _workOrderRepo.GetWorkOrder(request.WorkOrderId);
            if (workOrder is null)
                return ApiResponse<UpdateWorkOrderStatusCommandResponse>.GetNotFoundApiResponse("Work Order not found");

            workOrder.TaskStatusId = request.TaskStatusId;

            var isSuccess = await _workOrderRepo.UpdateWorkOrder(workOrder);
            if (isSuccess)
            {
                await _workOrderHub.Clients.All.SendAsync("ReceiveWorkOrderUpdate", "Status Updated");
            }

            return ApiResponse<UpdateWorkOrderStatusCommandResponse>.GetNoContentApiResponse();
        }
    }
}
