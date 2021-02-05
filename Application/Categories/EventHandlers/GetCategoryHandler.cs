using System.Threading;
using System.Threading.Tasks;
using Mapster;
using Market.Application.Categories.Queries;
using Market.Application.Interfaces;
using Market.Application.Models;
using Market.Domain.Exceptions;

namespace Market.Applications.Catalog.Cqrs.Categories
{
    public class GetCategoryHandler : IHandlerWrapper<GetCategory, CategoryDto>
    {
        private readonly IApplicationDbContext _db;

        public GetCategoryHandler(IApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ResponseViewModel<CategoryDto>> Handle(GetCategory request, CancellationToken cancellationToken)
        {
            var response = new ResponseViewModel<CategoryDto>();
            var category = await _db.Categories.FindAsync(new[] {request.Id}, cancellationToken);
            if (category is not null)
            {
                response.Entity = category.Adapt<CategoryDto>();
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