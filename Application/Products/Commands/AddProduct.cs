﻿using Market.Application.Interfaces;
using Market.Application.Models;

namespace Market.Application.Products.Commands
{
    public class AddProduct : IRequestWrapper<ProductDto>
    {
        public string ItemCode { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string ExtendedDescription { get; set; }
        public bool Active { get; set; }
        public string CategoryId { get; set; }
    }
}