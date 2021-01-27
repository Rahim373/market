using Market.Common.Entity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Catalog.Domain.Models
{
    [Index(nameof(Slug), IsUnique = true)]
    public class Product : BaseEntity
    {
        [MaxLength(32)]
        public string ItemCode { get; set; }
        [MaxLength(256)]
        public string Title { get; set; }
        [MaxLength(256)]
        public string Slug { get; set; }
        [MaxLength(1024)]
        public string Description { get; set; }
        [MaxLength(int.MaxValue)]
        public string ExtendedDescription { get; set; }
        public bool Active { get; set; }
        public string CategoryId { get; set; }
        [MaxLength(128)]
        public string Image { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
