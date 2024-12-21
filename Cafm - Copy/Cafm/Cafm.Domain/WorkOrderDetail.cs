using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafm.Domain
{
    public class WorkOrderDetail : BaseEntity
    {
        public int Id { get; set; }
        public long WorkOrderId { get; set; }
        public int CompanyId { get; set; }
        public int LocationId { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public int? Zoom { get; set; }
        public string? MapTypeId { get; set; }
        public string? ImgUrl { get; set; }
        public string? VoiceUrl { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? FileUrl { get; set; }
        public string IsDeleted { get; set; }

        // Navigation property for related WorkOrder
        public WorkOrder WorkOrder { get; set; }
    }
}
