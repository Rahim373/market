using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Market.Domain.Enums;

namespace Market.Domain.Entities
{

    public class ContactDetail : BaseEntity
    {
        [Required]
        public ContactType Type { get; set; }
        [MaxLength(64)]
        public string Name { get; set; }
        [MaxLength(128)]
        public string AddressLine1 { get; set; }
        [MaxLength(128)]
        public string AddressLine2 { get; set; }
        [MaxLength(64)]
        public string Province { get; set; }
        [Required]
        [MaxLength(64)]
        public string City { get; set; }
        [MaxLength(64)]
        [ForeignKey(nameof(Country))]
        public string CountryId { get; set; }
        [DataType(DataType.PostalCode)]
        public string Zip { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string ContactNumber { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public virtual Country Country { get; set; }
        public virtual ContactDetail RetailerContactDetail { get; set; }
    }
}
