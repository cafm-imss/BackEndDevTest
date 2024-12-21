using System;
using System.Collections.Generic;

namespace CAFM.Database.Models;

public partial class TaskPriority
{
    public int PriorityId { get; set; }

    public int CompanyId { get; set; }

    public string PriorityName { get; set; } = null!;

    public string? PriorityNameEn { get; set; }

    public string PriorityColor { get; set; } = null!;

    public bool IsDefault { get; set; }

    public byte PriorityOrder { get; set; }

    public long? LocationId { get; set; }

    public int ResponseRateMinutes { get; set; }

    public int CompletionRateMinutes { get; set; }

    public int DueDateIntervalMinutes { get; set; }

    public virtual ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();
}
