using Market.Common.Entity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Catalog.Domain.Models
{
    [Index(nameof(Slug), IsUnique = true)]
    public class Category : BaseEntity
    {
        [MaxLength(64)]
        public string Title { get; set; }
        [MaxLength(64)]
        public string Slug { get; set; }
        [MaxLength(1024)]
        public string Description { get; set; }
        public bool Active { get; set; }
        [MaxLength(255)]
        public string Image { get; set; }

        [ForeignKey(nameof(ParentCategory))]
        public string ParentCategoryId { get; set; }
        public virtual Category ParentCategory { get; set; }
    }
}
