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

        public async Task<WorkOrder> GetWorkOrderTaskAndAsset(long id)
            => await _context.WorkOrders
            .AsNoTracking()
            .Where(a => a.Id == id && !a.IsDeleted)
            .Select(a => new WorkOrder
            {
                Id = a.Id,
                CompanyId = a.CompanyId,
                LocationId = a.LocationId,
                InternalNumber = a.InternalNumber,
                TaskName = a.TaskName,
                TaskDescription = a.TaskDescription,
                StartDate = a.StartDate,
                DueDate = a.DueDate,
                TaskAssignmentId = a.TaskAssignmentId,
                EstimatedTime = a.EstimatedTime,
                TaskTypeId = a.TaskTypeId,
                AssetId = a.AssetId,
                Asset = a.Asset == null ? null : new Asset
                {
                    Id = a.Asset.Id,
                    AssetName = a.Asset.AssetName,
                    Notes = a.Asset.Notes,
                    ParentId = a.Asset.ParentId,
                    InternalId = a.Asset.InternalId,
                    AssetOrder = a.Asset.AssetOrder,
                    CompanyId = a.Asset.CompanyId,
                    LocationId = a.Asset.LocationId,
                    CategoryId = a.Asset.CategoryId,
                    ImagePath = a.Asset.ImagePath,
                    WeeklyOperationHours = a.Asset.WeeklyOperationHours
                },
                TaskStatus = a.TaskStatus == null ? null : new Domain.Entities.TaskStatue
                {
                    Id = a.TaskStatus.Id,
                    StatusName = a.TaskStatus.StatusName,
                    IsCompleted = a.TaskStatus.IsCompleted,
                    StatusNameEn = a.TaskStatus.StatusNameEn,
                    IsStart = a.TaskStatus.IsStart
                },
                CompletionDate = a.CompletionDate,
                CompletionNote = a.CompletionNote,
                CompletionRatio = a.CompletionRatio,
                AssetDownTime = a.AssetDownTime
            })
            .FirstOrDefaultAsync();

        public async Task<WorkOrder> GetWorkOrder(long id)
            => await _context.WorkOrders
            .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);

        public async Task<bool> UpdateWorkOrder(WorkOrder workOrder)
        {
            _context.WorkOrders.Update(workOrder);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
