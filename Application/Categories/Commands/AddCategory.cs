using Market.Application.Interfaces;
using Market.Application.Models;

namespace Market.Application.Categories.Commands
{
    public class AddCategory : IRequestWrapper<CategoryDto>
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public string ParentCategoryId { get; set; }
    }
}