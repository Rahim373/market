using Market.Catalog.Applications.Products.Dtos;
using Market.Catalog.Domain.Context;
using Market.Catalog.Domain.Models;
using Market.Common;
using Market.Common.ViewModels;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Market.Catalog.Applications.Products.Cqrs
{
    public class List
    {
        public class Query : GridFilterViewModel, IRequestWrapper<GridResponseViewModel<ProductDto>>
        {
        }

        public class Handler : IHandlerWrapper<Query, GridResponseViewModel<ProductDto>>
        {
            private readonly CatalogDbContext _db;

            public Handler(CatalogDbContext db)
            {
                _db = db;
            }

            public async Task<ResponseViewModel<GridResponseViewModel<ProductDto>>> Handle(Query request,
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
}