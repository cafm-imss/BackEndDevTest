using Cafm.Common.WorkOrder;
using Cafm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafm.Application.Interfaces
{
    public interface IWorkOrderService
    {
        Task<BaseCommandResponse<bool>> SaveWorkOrderAsync(WorkOrderDto workOrderDto);
        Task<BaseCommandResponse<WorkOrderDto>> GetWorkOrderByIdAsync(long id);
        Task<BaseCommandResponse<bool>> UpdateWorkOrderStatusAsync(long id, int newStatusId);
    }
}
