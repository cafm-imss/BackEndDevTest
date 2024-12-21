using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cafm.Domain
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            CreatedOn = DateTime.UtcNow;
        }

        [JsonIgnore]

        public bool? IsDeleted { get; set; }
        [JsonIgnore]

        public bool? IsActive { get; set; }
        [JsonIgnore]

        public DateTime? CreatedOn { get; set; }
        [JsonIgnore]
        public Guid? CreatedBy { get; set; }

        [JsonIgnore]
        public DateTime? UpdatedOn { get; set; }
        [JsonIgnore]
        public Guid? UpdatedBy { get; set; }
        [JsonIgnore]
        public DateTime? DeletedOn { get; set; }

    }
}
