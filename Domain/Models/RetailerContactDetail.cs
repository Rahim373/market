using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Domain.Entities
{
    public class RetailerContactDetail : BaseEntity
    {
        [Required]
        [ForeignKey(nameof(Retailer))]
        public string RetailerId { get; set; }
        [Required]
        [ForeignKey(nameof(ContactDetail))]
        public string ContactDetailerId { get; set; }

        public virtual Retailer Retailer { get; set; }
        public virtual ContactDetail ContactDetail { get; set; }
    }
}
