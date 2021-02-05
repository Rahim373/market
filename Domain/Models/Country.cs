using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Market.Domain.Entities
{
    [Index(nameof(CountryCode), IsUnique = true)]
    [Index(nameof(Name), IsUnique = true)]
    public class Country : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string DialCode { get; set; }
        [Required]
        public string CountryCode { get; set; }

        public virtual ICollection<ContactDetail> ContactDetails { get; set; }
    }
}
