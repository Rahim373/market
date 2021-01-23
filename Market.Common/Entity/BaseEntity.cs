using System;
using System.ComponentModel.DataAnnotations;

namespace Market.Common.Entity
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid().ToString().ToLower();
        }
        
        [Key]
        public string Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
