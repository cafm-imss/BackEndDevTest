using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafm.Domain
{
    public class WorkOrder : BaseEntity
    {
        public long Id { get; set; }
        public int CompanyId { get; set; }
        public long LocationId { get; set; }
        public long InternalNumber { get; set; }
        public string TaskName { get; set; }
        public string? TaskDescription { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public long? TaskAssignmentId { get; set; }
        public int EstimatedTime { get; set; } = 0;
        public short TaskTypeId { get; set; }
        public long? AssetId { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string? CompletionNote { get; set; }
        public int PriorityId { get; set; }
        public int CompletionRatio { get; set; } = 0;
        public int TaskStatusId { get; set; }
        public int AssetDownTime { get; set; } = 0;
        public bool IsDeleted { get; set; } = false;
        public long CreatedBy { get; set; }

        // Navigation properties for related TaskPriority, TaskStatus, Asset
        public TaskPriority TaskPriority { get; set; }
        public TaskStatus TaskStatus { get; set; }
        public Asset? Asset { get; set; }
        public ICollection<WorkOrderDetail> WorkOrderDetails { get; set; }
    }
}
