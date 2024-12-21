using System;
using System.Collections.Generic;

namespace CAFM.Database.Models;

public partial class MessagesSystem
{
    public short MsgId { get; set; }

    public string Lang { get; set; } = null!;

    public string MsgText { get; set; } = null!;
}
