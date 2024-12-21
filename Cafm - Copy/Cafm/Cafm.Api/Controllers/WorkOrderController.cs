using Cafm.Application.Interfaces;
using Cafm.Common.WorkOrder;
using Microsoft.AspNetCore.Mvc;

namespace Cafm.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkOrderController : ControllerBase
    {
        private readonly IWorkOrderService _workOrderService;

        public WorkOrderController(IWorkOrderService workOrderService)
        {
            _workOrderService = workOrderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorkOrder([FromBody] WorkOrderDto workOrderDto)
        {
            var response = await _workOrderService.SaveWorkOrderAsync(workOrderDto);
            if (response.IsSuccess)
                return CreatedAtAction(nameof(GetWorkOrderById), new { id = response.ResponseData }, response);
            return BadRequest(response.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkOrderById(long id)
        {
            var response = await _workOrderService.GetWorkOrderByIdAsync(id);
            if (response.IsSuccess)
                return Ok(response);
            return NotFound(response.Message);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateWorkOrderStatus(long id, [FromBody] int newStatusId)
        {
            var response = await _workOrderService.UpdateWorkOrderStatusAsync(id, newStatusId);
            if (response.IsSuccess)
                return Ok(response);
            return NotFound(response.Message);
        }
    }

    }
