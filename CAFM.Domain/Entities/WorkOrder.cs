namespace CAFM.Domain.Entities;

public class WorkOrder
{
    public long Id { get; set; }

    public int CompanyId { get; set; }

    public long LocationId { get; set; }

    public long InternalNumber { get; set; }

    public string TaskName { get; set; }

    public string TaskDescription { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime DueDate { get; set; }

    public long? TaskAssignmentId { get; set; }

    public int EstimatedTime { get; set; }

    public short TaskTypeId { get; set; }

    public long? AssetId { get; set; }

    public DateTime? CompletionDate { get; set; }

    public string CompletionNote { get; set; }

    public int PriorityId { get; set; }

    public int CompletionRatio { get; set; }

    public int TaskStatusId { get; set; }

    public int AssetDownTime { get; set; }

    public bool IsDeleted { get; set; }

    public long CreatedBy { get; set; }

    public virtual Asset Asset { get; set; }

    public virtual TaskPriority Priority { get; set; }

    public virtual TaskStatue TaskStatus { get; set; }

    public virtual ICollection<WorkOrderDetail> WorkOrderDetails { get; set; } = new List<WorkOrderDetail>();
}
