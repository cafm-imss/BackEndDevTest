using CAFM.Application.Features.WorkOrder.Commands.UpdateWorkOrderStatus;
using CAFM.Application.Features.WorkOrder.Queries.GetWorkOrder;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CAFM.API.Controllers
{
    public class WorkOrderController : BaseController
    {
        private readonly IMediator _mediator;

        public WorkOrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("api/workorder/{id}")]
        [ProducesResponseType(typeof(GetWorkOrderQueryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetWorkOrderQueryResponse>> GetWorkOrder(long id)
        {
            var result = await _mediator.Send(new GetWorkOrderQuery(id));

            return GetApiResponse(result);
        }

        [HttpPut("/api/workorder/{id}/status")]
        [ProducesResponseType(typeof(UpdateWorkOrderStatusCommandResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UpdateWorkOrderStatusCommandResponse>> UpdateWorkOrderStatus(long id, [FromBody] UpdateWorkOrderStatusCommand command)
        {
            command.WorkOrderId = id;

            var result = await _mediator.Send(command);

            return GetApiResponse(result);
        }
    }
}
