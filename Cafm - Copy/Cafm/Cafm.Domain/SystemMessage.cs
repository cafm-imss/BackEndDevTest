using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafm.Domain
{
    public class SystemMessage : BaseEntity
    {
        public short MsgId { get; set; }
        public string Lang { get; set; }
        public string MsgText { get; set; }
    }
}
