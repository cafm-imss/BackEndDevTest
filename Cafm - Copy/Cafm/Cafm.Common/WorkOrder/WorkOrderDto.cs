using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafm.Common.WorkOrder
{
    public class WorkOrderDto
    {
        public long Id { get; set; }
        public int CompanyId { get; set; }
        public long LocationId { get; set; }
        public long InternalNumber { get; set; } // Added InternalNumber
        public string TaskName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int PriorityId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } // CreatedDate for when the work order was created

        // List of WorkOrderDetails
        public List<WorkOrderDetailDto> Details { get; set; } = new List<WorkOrderDetailDto>();
    }
}
