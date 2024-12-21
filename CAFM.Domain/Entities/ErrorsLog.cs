namespace CAFM.Domain.Entities;

public class ErrorsLog
{
    public int Id { get; set; }

    public int CompanyId { get; set; }

    public string UserName { get; set; }

    public string LocalHost { get; set; }

    public string ErrSource { get; set; }

    public string ErrMsg { get; set; }

    public byte ErrFrom { get; set; }

    public DateTime ErrTime { get; set; }

    public string Serial { get; set; }

    public string Notes { get; set; }

    public DateTime? SendByEmail { get; set; }
}
