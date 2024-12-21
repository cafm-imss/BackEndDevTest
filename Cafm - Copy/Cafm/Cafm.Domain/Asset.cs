using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafm.Domain
{
    public class Asset : BaseEntity
    {
        public long Id { get; set; }
        public int CompanyId { get; set; }
        public long LocationId { get; set; }
        public string AssetName { get; set; }
        public long? CategoryId { get; set; }
        public string? ImagePath { get; set; }
        public byte? WeeklyOperationHours { get; set; } = 50;
        public bool IsDeleted { get; set; } = false;
        public long? ParentId { get; set; }
        public long? InternalId { get; set; }
        public short? AssetOrder { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public string? Notes { get; set; }

        public ICollection<WorkOrder> WorkOrders { get; set; }

    }
}
