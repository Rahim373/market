using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Market.Domain.Entities
{
    [Index(nameof(Code), IsUnique = true)]
    [Index(nameof(UserId), IsUnique = true)]
    public class Referral : BaseEntity
    {
        [Required]
        [MaxLength(32)]
        public string Code { get; set; }
        
        [ForeignKey(nameof(User))]
        [Required]
        public string UserId { get; set; }
        [MaxLength(32)]
        public string ReferredBy { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}