using CAFM.Core.Services;
using CAFM.Database.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CAFM.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class WorkOrderController : ControllerBase
{
    private readonly WorkOrderService _workOrderService;

    public WorkOrderController(WorkOrderService workOrderService)
    {
        _workOrderService = workOrderService;
    }
    //We should use Dto instead of entity directly!

    // POST /api/workorder
    [HttpPost]
    public async Task<IActionResult> CreateWorkOrder([FromBody] WorkOrder workOrder)
    {
        if (workOrder == null)
        {
            return BadRequest(new { Message = "Invalid work order data" });
        }

        try
        {
            var result = await _workOrderService.SaveWorkOrderAsync(workOrder);
            return CreatedAtAction(nameof(GetWorkOrderById), new { id = workOrder.ID }, new { Message = result });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    // GET /api/workorder/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetWorkOrderById(long id)
    {
        try
        {
            var workOrder = await _workOrderService.GetWorkOrderByIdAsync(id);
            if (workOrder == null)
            {
                return NotFound(new { Message = "Work order not found" });
            }

            return Ok(workOrder);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    // PUT /api/workorder/{id}/status
    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateWorkOrderStatus(long id, [FromBody] string status)
    {
        if (string.IsNullOrWhiteSpace(status))
        {
            return BadRequest(new { Message = "Status is required" });
        }

        try
        {
            await _workOrderService.UpdateWorkOrderStatusAsync(id, status);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { Message = "Work order not found" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}