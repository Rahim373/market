using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Market.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(64)]
        public string FirstName { get; set; }
        [MaxLength(64)]
        public string LastName { get; set; }
        [Required]
        public bool Active { get; set; }
        [Required]
        public int Point { get; set; }

        public virtual Referral Referral { get; set; }
        public virtual ICollection<PointHistory> ReferralHistories { get; set; }
    }
}