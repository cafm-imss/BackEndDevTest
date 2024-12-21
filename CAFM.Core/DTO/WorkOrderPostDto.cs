namespace CAFM.Core.DTO
{
    public class WorkOrderPostDto
    {
        public int CompanyId { get; set; }

        public long LocationId { get; set; }

        public string TaskName { get; set; } = null!;

        public string? TaskDescription { get; set; }

        public DateTime DueDate { get; set; }

        public long? TaskAssignmentId { get; set; }

        public int EstimatedTime { get; set; }

        public short TaskTypeId { get; set; }

        public long? AssetId { get; set; } // Asset ID

        public int PriorityId { get; set; } // Priority ID

        public int TaskStatusId { get; set; } // Task Status ID

        public int AssetDownTime { get; set; }

        public long CreatedBy { get; set; }
    }
}
