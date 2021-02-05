using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Market.Domain.Entities
{
    [Index(nameof(RetailerCode), IsUnique = true)]
    public class Retailer : BaseEntity
    {
        [MaxLength(32)]
        [Required]
        public string RetailerCode { get; set; }
        [MaxLength(256)]
        public string RetailerName { get; set; }
        [MaxLength(256)]
        public string CompanyName { get; set; }
        [MaxLength(256)]
        public string Image { get; set; }
        [Required]
        public string TimeZoneId { get; set; }
        [Required]
        public bool Active { get; set; }
        [MaxLength(1024)]
        public string Note { get; set; }

        public virtual ICollection<RetailerDocument> Documents { get; set; }
        public virtual ICollection<RetailerContactDetail> RetailerContactDetails { get; set; }
        public virtual ICollection<RetailerProduct> RetailerProducts { get; set; }
    }
}
