namespace CAFM.Application.Features.WorkOrder.Queries.GetWorkOrder
{
    public class GetWorkOrderQueryResponse
    {
        public long Id { get; set; }

        public int CompanyId { get; set; }

        public long LocationId { get; set; }

        public long InternalNumber { get; set; }

        public string TaskName { get; set; }

        public string TaskDescription { get; set; }

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

        public int AssetDownTime { get; set; }

        public GetWorkOrderTaskStatus TaskStatus { get; set; }
        public GetWorkOrderAsset? Asset { get; set; }
    }
    public class GetWorkOrderAsset
    {
        public long Id { get; set; }

        public string Notes { get; set; }
        public long? ParentId { get; set; }

        public long? InternalId { get; set; }

        public short? AssetOrder { get; set; }


        public int CompanyId { get; set; }

        public long LocationId { get; set; }

        public string AssetName { get; set; }

        public long? CategoryId { get; set; }

        public string ImagePath { get; set; }

        public byte? WeeklyOperationHours { get; set; }
    }
    public class GetWorkOrderTaskStatus
    {
        public long Id { get; set; }
        public string StatusName { get; set; }
        public string StatusNameEn { get; set; }
        public bool IsStart { get; set; }
        public bool IsCompleted { get; set; }
    }
}