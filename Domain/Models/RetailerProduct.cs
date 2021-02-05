using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Domain.Entities
{
    public class RetailerProduct : BaseEntity
    {
        [Required]
        [ForeignKey(nameof(Retailer))]
        public string RetailerId { get; set; }
        [Required]
        [ForeignKey(nameof(Product))]
        public string ProductId { get; set; }
        public bool Active { get; set; }
        public bool EnableInventory { get; set; }
        public bool EnableBackOrder { get; set; }


        public virtual Retailer Retailer { get; set; }
        public virtual Product Product { get; set; }
    }
}
