using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Market.Application.Helpers;
using Market.Application.Interfaces;
using Market.Application.Models;
using Market.Application.Products.Queries;
using Market.Domain.Entities;

namespace Market.Application.Products.EventHandlers
{
    public class GetCategoriesWithPaginationHandler: IHandlerWrapper<GetProductsWithPagination, GridResponseViewModel<ProductDto>>
    {
        private readonly IApplicationDbContext _db;

        public GetCategoriesWithPaginationHandler(IApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ResponseViewModel<GridResponseViewModel<ProductDto>>> Handle(GetProductsWithPagination request,
            CancellationToken cancellationToken)
        {
            var response = new ResponseViewModel<GridResponseViewModel<ProductDto>>();

            var products = _db.Categories
                .OrderBy(c => c.DateCreated);

            response.Entity = new PagedListHelper<Category>(products).ToPagedList<ProductDto>(request);

            return await Task.FromResult(response);
        }
    }
}