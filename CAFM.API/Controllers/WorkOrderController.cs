using CAFM.Core.DTO;
using CAFM.Core.Interfaces;
using CAFM.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CAFM.API.Controllers
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
        public async Task<IActionResult> CreateWorkOrder([FromBody] WorkOrderPostDto workOrderDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var workOrder = new WorkOrder
            {
                CompanyId = workOrderDto.CompanyId,
                LocationId = workOrderDto.LocationId,
                TaskName = workOrderDto.TaskName,
                TaskDescription = workOrderDto.TaskDescription,
                DueDate = workOrderDto.DueDate,
                TaskAssignmentId = workOrderDto.TaskAssignmentId,
                EstimatedTime = workOrderDto.EstimatedTime,
                TaskTypeId = workOrderDto.TaskTypeId,
                AssetId = workOrderDto.AssetId,
                PriorityId = workOrderDto.PriorityId,
                TaskStatusId = workOrderDto.TaskStatusId,
                AssetDownTime = workOrderDto.AssetDownTime,
                CreatedBy = workOrderDto.CreatedBy,
                CreatedDate = DateTime.UtcNow
            };
            var workOrderId = await _workOrderService.SaveWorkOrderAsync(workOrder);
            return CreatedAtAction(nameof(GetWorkOrderById), new { id = workOrderId }, workOrder);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkOrderById(long id)
        {
            var workOrder = await _workOrderService.GetWorkOrderByIdAsync(id);
            if (workOrder == null)
                return NotFound();

            return Ok(workOrder);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateWorkOrderStatus(long id, [FromBody] int statusUpdate)
        {
            var result = await _workOrderService.UpdateWorkOrderStatusAsync(id, statusUpdate);
            if (result)
                return NoContent();

            return NotFound();
        }
    }
}
