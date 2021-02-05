using Mapster;
using System.Threading;
using System.Threading.Tasks;
using Market.Application.Categories.Commands;
using Market.Application.Categories.Interfaces;
using Market.Application.Interfaces;
using Market.Application.Models;
using Market.Domain.Entities;

namespace Market.Application.Categories.EventHandlers
{
    public class AddCategoryHandler : IHandlerWrapper<AddCategory, CategoryDto>
    {
        private readonly IApplicationDbContext _db;
        private readonly ICategoryManager _manager;

        public AddCategoryHandler(IApplicationDbContext db, ICategoryManager manager)
        {
            _db = db;
            _manager = manager;
        }

        public async Task<ResponseViewModel<CategoryDto>> Handle(AddCategory command,
            CancellationToken cancellationToken)
        {
            var response = new ResponseViewModel<CategoryDto>();

            if (string.IsNullOrEmpty(command.Title))
            {
                response.AddMessage("Invalid title", MessageType.Error);
                return await Task.FromResult(response);
            }

            if (!string.IsNullOrEmpty(command.ParentCategoryId))
            {
                var isParentExist = await _manager.IsParentExistsAsync(command.ParentCategoryId, cancellationToken);

                if (!isParentExist)
                {
                    response.AddMessage("Invalid parent category.", MessageType.Error);
                    return await Task.FromResult(response);
                }
            }

            var category = command.Adapt<Category>();
            category.Title = category.Title.Trim();
            category.Description = category.Description?.Trim();
            category.Slug = await _manager.GenerateUniqueSlugAsync(command.Slug ?? command.Title, null, cancellationToken);
            await _db.Categories.AddAsync(category, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);

            response.Entity = category.Adapt<CategoryDto>();
            response.Succeed();
            response.AddMessage("Successfully saved.", MessageType.Success);

            return await Task.FromResult(response);
        }
    }
}