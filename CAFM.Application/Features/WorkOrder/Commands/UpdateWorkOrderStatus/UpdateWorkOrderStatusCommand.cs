using CAFM.Application.Responses;
using MediatR;

namespace CAFM.Application.Features.WorkOrder.Commands.UpdateWorkOrderStatus
{
    public class UpdateWorkOrderStatusCommand : IRequest<ApiResponse<UpdateWorkOrderStatusCommandResponse>>
    {
        public long WorkOrderId { get; set; }
        public int TaskStatusId { get; set; }
    }
}
