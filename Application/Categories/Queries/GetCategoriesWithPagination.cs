using Market.Application.Interfaces;
using Market.Application.Models;

namespace Market.Application.Categories.Queries
{
    public class GetCategoriesWithPagination : GridFilterViewModel, IRequestWrapper<GridResponseViewModel<CategoryDto>>
    {
    }
}