
using CAFM.Domain.Entities;

namespace CAFM.Application.Contracts.Repos
{
    public interface IWorkOrderRepo
    {
        Task<WorkOrder> GetWorkOrder(long id);
    }
}
