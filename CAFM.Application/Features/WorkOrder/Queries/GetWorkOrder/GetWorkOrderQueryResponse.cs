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

    }
}