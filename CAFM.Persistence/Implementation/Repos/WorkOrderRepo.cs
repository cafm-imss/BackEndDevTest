using CAFM.Application.Contracts.Repos;
using CAFM.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CAFM.Persistence.Implementation.Repos
{
    internal class WorkOrderRepo : IWorkOrderRepo
    {
        private readonly CAFMDbContext _context;

        public WorkOrderRepo(CAFMDbContext context)
        {
            _context = context;
        }

        public async Task<WorkOrder> GetWorkOrder(long id)
            => await _context.WorkOrders
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);
    }
}
