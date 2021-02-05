using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Domain.Entities
{
    public class PointHistory : BaseEntity
    {
        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        [Required]
        public int PreviousPoint { get; set; }
        [Required]
        public int CurrentPoint { get; set; }
        [Required]
        [MaxLength(128)]
        public string Reason { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}