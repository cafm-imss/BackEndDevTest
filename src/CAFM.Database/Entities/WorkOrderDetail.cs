using System;
using System.Collections.Generic;

namespace CAFM.Database.Entities;

public partial class WorkOrderDetail
{
    public int ID { get; set; }

    public long WorkOrderId { get; set; }

    public int CompanyId { get; set; }

    public int LocationId { get; set; }

    public string? Latitude { get; set; }

    public string? Longitude { get; set; }

    public int? Zoom { get; set; }

    public string? MapTypeId { get; set; }

    public string? ImgURL { get; set; }

    public string? VoiceURL { get; set; }

    public long CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? FileURL { get; set; }

    public string IsDeleted { get; set; } = null!;

    public virtual WorkOrder WorkOrder { get; set; } = null!;
}
