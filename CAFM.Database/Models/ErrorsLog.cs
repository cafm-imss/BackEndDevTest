using System;
using System.Collections.Generic;

namespace CAFM.Database.Models;

public partial class ErrorsLog
{
    public int Id { get; set; }

    public int CompanyId { get; set; }

    public string UserName { get; set; } = null!;

    public string? LocalHost { get; set; }

    public string ErrSource { get; set; } = null!;

    public string ErrMsg { get; set; } = null!;

    public byte ErrFrom { get; set; }

    public DateTime ErrTime { get; set; }

    public string? Serial { get; set; }

    public string? Notes { get; set; }

    public DateTime? SendByEmail { get; set; }
}
