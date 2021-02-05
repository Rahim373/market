using System;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using Market.Application.Categories.Commands;
using Market.Application.Categories.Interfaces;
using Market.Application.Interfaces;
using Market.Application.Models;

namespace Market.Applications.Catalog.Cqrs.Categories
{
    public class Edit
    {
        public class UpdateCategoryHandler : IHandlerWrapper<UpdateCategory, CategoryDto>
        {
            private readonly IApplicationDbContext _db;
            private readonly ICategoryManager _manager;

            public UpdateCategoryHandler(IApplicationDbContext db, ICategoryManager manager)
            {
                _db = db;
                _manager = manager;
            }

            public async Task<ResponseViewModel<CategoryDto>> Handle(UpdateCategory command,
                CancellationToken cancellationToken)
            {
                var response = new ResponseViewModel<CategoryDto>();

                if (string.IsNullOrEmpty(command.Title))
                {
                    response.AddMessage("Invalid title.", MessageType.Error);
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
                    else if (command.Id.ToLower() == command.ParentCategoryId.ToLower())
                    {
                        response.AddMessage("Same item can not be its parent.", MessageType.Error);
                        return await Task.FromResult(response);
                    }
                }

                var category = await _db.Categories.FindAsync(new[] {command.Id}, cancellationToken);

                if (category is null)
                {
                    response.AddMessage("Invalid category.", MessageType.Error);
                    return await Task.FromResult(response);
                }

                category.Title = command.Title.Trim();
                category.Description = command.Description?.Trim();
                category.Slug =
                    await _manager.GenerateUniqueSlugAsync(command.Slug ?? command.Title, null, cancellationToken);
                category.Active = command.Active;

                await _db.SaveChangesAsync(cancellationToken);

                response.Entity = category.Adapt<CategoryDto>();
                response.Succeed();
                response.AddMessage("Successfully updated.", MessageType.Success);

                return await Task.FromResult(response);
            }
        }
    }
}