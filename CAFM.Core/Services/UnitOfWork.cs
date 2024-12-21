using CAFM.Core.Interfaces;
using CAFM.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace CAFM.Core.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CmmsBeTestContext _context;
        public IBaseRepository<WorkOrder> WorkOrderRepository { get; set; }
        public IBaseRepository<TaskStatue> TaskStatueRepository { get; set; }
        // New repositories for the last models
        public IBaseRepository<Asset> AssetRepository { get; set; }
        public IBaseRepository<TaskPriority> TaskPriorityRepository { get; set; }
        public IBaseRepository<WorkOrderDetail> WorkOrderDetailRepository { get; set; }

        public UnitOfWork(CmmsBeTestContext context)
        {
            _context = context;
            WorkOrderRepository = new BaseRepository<WorkOrder>(context);
            TaskStatueRepository = new BaseRepository<TaskStatue>(context);
            // Initialize repositories for the new models
            AssetRepository = new BaseRepository<Asset>(context);
            TaskPriorityRepository = new BaseRepository<TaskPriority>(context);
            WorkOrderDetailRepository = new BaseRepository<WorkOrderDetail>(context);
        }
        public DbContext GetDbContext() => _context;

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
