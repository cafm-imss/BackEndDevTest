using System;
using System.Collections.Generic;

namespace CAFM.Database.Models;

public partial class Asset
{
    public long Id { get; set; }

    public int CompanyId { get; set; }

    public long LocationId { get; set; }

    public string AssetName { get; set; } = null!;

    public long? CategoryId { get; set; }

    public string? ImagePath { get; set; }

    public byte? WeeklyOperationHours { get; set; }

    public bool IsDeleted { get; set; }

    public long? ParentId { get; set; }

    public long? InternalId { get; set; }

    public short? AssetOrder { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Notes { get; set; }

    public virtual ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();
}
