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

        [HttpGet("api/workorder/{id}", Name = "GetWorkOrder")]
        [ProducesResponseType(typeof(GetWorkOrderQueryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetWorkOrderQueryResponse>> GetWorkOrder(long id)
        {
            var result = await _mediator.Send(new GetWorkOrderQuery(id));

            return GetApiResponse(result);
        }
    }
}
