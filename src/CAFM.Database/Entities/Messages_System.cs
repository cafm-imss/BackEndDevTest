using System;
using System.Collections.Generic;

namespace CAFM.Database.Entities;

public partial class Messages_System
{
    public short MsgID { get; set; }

    public string Lang { get; set; } = null!;

    public string MsgText { get; set; } = null!;
}
