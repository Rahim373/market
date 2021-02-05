using System.Threading;
using System.Threading.Tasks;
using Mapster;
using Market.Application.Interfaces;
using Market.Application.Models;
using Market.Application.Products.Queries;
using Market.Domain.Exceptions;

namespace Market.Applications.Products.Categories
{
    public class GetProductHandler : IHandlerWrapper<GetProduct, ProductDto>
    {
        private readonly IApplicationDbContext _db;

        public GetProductHandler(IApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ResponseViewModel<ProductDto>> Handle(GetProduct request, CancellationToken cancellationToken)
        {
            var response = new ResponseViewModel<ProductDto>();
            var product = await _db.Products.FindAsync(new[] {request.Id}, cancellationToken);
            if (product is not null)
            {
                response.Entity = product.Adapt<ProductDto>();
                response.Succeed();
            }
            else
            {
                throw new NotFoundException();
            }

            return await Task.FromResult(response);
        }
    }
}