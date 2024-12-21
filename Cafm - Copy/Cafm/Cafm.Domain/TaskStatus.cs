using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafm.Domain
{
    public class TaskStatus : BaseEntity
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string StatusName { get; set; }
        public string? StatusNameEn { get; set; }
        public bool IsStart { get; set; } = false;
        public bool IsCompleted { get; set; } = false;
        public bool IsCheckInstructions { get; set; } = false;
        public bool IsMandatory { get; set; } = false;
        public byte StatusOrder { get; set; }
        public bool IsRejected { get; set; } = false;

        public ICollection<WorkOrder> WorkOrders { get; set; }

    }
}
