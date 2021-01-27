using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Market.Applications.Products.Dtos;
using Market.Common;
using Market.Common.ViewModels;
using Market.Domain.Context;
using Market.Domain.Models;

namespace Market.Applications.Products.Cqrs
{
    public class List
    {
        public class Query : GridFilterViewModel, IRequestWrapper<GridResponseViewModel<ProductDto>>
        {
        }

        public class Handler : IHandlerWrapper<Query, GridResponseViewModel<ProductDto>>
        {
            private readonly MarketDbContext _db;

            public Handler(MarketDbContext db)
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