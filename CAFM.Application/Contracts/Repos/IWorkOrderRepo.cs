
using CAFM.Domain.Entities;

namespace CAFM.Application.Contracts.Repos
{
    public interface IWorkOrderRepo
    {
        Task<WorkOrder> GetWorkOrderTaskAndAsset(long id);
        Task<WorkOrder> GetWorkOrder(long id);
        Task<bool> UpdateWorkOrder(WorkOrder workOrder);
    }
}
