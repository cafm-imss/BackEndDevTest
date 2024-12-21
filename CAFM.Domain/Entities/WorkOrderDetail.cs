namespace CAFM.Domain.Entities;

public class WorkOrderDetail
{
    public int Id { get; set; }

    public long WorkOrderId { get; set; }

    public int CompanyId { get; set; }

    public int LocationId { get; set; }

    public string Latitude { get; set; }

    public string Longitude { get; set; }

    public int? Zoom { get; set; }

    public string MapTypeId { get; set; }

    public string ImgUrl { get; set; }

    public string VoiceUrl { get; set; }

    public long CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string FileUrl { get; set; }

    public string IsDeleted { get; set; }

    public virtual WorkOrder WorkOrder { get; set; }
}
