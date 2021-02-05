using Market.Application.Interfaces;
using Market.Application.Models;

namespace Market.Application.Products.Queries
{
    public class GetProductsWithPagination : GridFilterViewModel, IRequestWrapper<GridResponseViewModel<ProductDto>>
    {
    }
}