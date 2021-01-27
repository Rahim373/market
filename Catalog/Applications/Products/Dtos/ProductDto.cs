using System;

namespace Market.Catalog.Applications.Products.Dtos
{
    public class ProductDto
    {
        public string Id { get; set; }
        public string ItemCode { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string ExtendedDescription { get; set; }
        public bool Active { get; set; }
        public string CategoryId { get; set; }
        public string Image { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
