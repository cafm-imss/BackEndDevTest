using CAFM.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace CAFM.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IBaseRepository<WorkOrder> WorkOrderRepository { get; }
        IBaseRepository<TaskStatue> TaskStatueRepository { get; }
        IBaseRepository<Asset> AssetRepository { get; }
        IBaseRepository<TaskPriority> TaskPriorityRepository { get; }
        IBaseRepository<WorkOrderDetail> WorkOrderDetailRepository { get; }
        //-----------------------------------------------------------------------------------
        int SaveChanges();
        DbContext GetDbContext();

        Task<int> SaveChangesAsync();
    }

}
