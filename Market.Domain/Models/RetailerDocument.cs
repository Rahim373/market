using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Domain.Models
{
    public enum RetailerDocumentType
    {
        Passport = 0,
        NID = 1,
        TIN = 2
    }

    public class RetailerDocument : BaseEntity
    {
        [ForeignKey(nameof(Retailer))]
        public string RetailerId { get; set; }
        [MaxLength(256)]
        public string DocumentUrl { get; set; }
        [Required]
        public bool IsValidated { get; set; }
        public DateTime? ValidatedOn { get; set; }
        [Required]
        public RetailerDocumentType DocumentType { get; set; }
        [MaxLength(256)]
        [Required]
        public string DocumentValue { get; set; }
        public DateTime? DocumentWillExpireOn { get; set; }

        public virtual Retailer Retailer { get; set; }
    }
}
