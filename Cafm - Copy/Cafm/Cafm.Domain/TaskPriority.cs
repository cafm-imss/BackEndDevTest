using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafm.Domain
{
    public class TaskPriority : BaseEntity
    {
        [Key]
        public int PriorityId { get; set; }
        public int CompanyId { get; set; }
        public string PriorityName { get; set; }
        public string? PriorityNameEn { get; set; }
        public string PriorityColor { get; set; }
        public bool IsDefault { get; set; }
        public byte PriorityOrder { get; set; }
        public long? LocationId { get; set; }
        public int ResponseRateMinutes { get; set; } = 0;
        public int CompletionRateMinutes { get; set; } = 0;
        public int DueDateIntervalMinutes { get; set; } = 0;

        public ICollection<WorkOrder> WorkOrders { get; set; }

    }
}
