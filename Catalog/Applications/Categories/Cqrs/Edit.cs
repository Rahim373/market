using System;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using Market.Catalog.Applications.Categories.Dtos;
using Market.Catalog.Applications.Categories.Manager;
using Market.Catalog.Domain.Context;
using Market.Common;
using Microsoft.EntityFrameworkCore;

namespace Market.Catalog.Applications.Categories.Cqrs
{
    public class Edit
    {
        public class Command : IRequestWrapper<CategoryDto>
        {
            public string Id { get; set; }
            public string Title { get; set; }
            public string Slug { get; set; }
            public string Description { get; set; }
            public bool Active { get; set; }
            public string ParentCategoryId { get; set; }
        }

        public class Handler : IHandlerWrapper<Command, CategoryDto>
        {
            private readonly CatalogDbContext _db;
            private readonly ICategoryManager _manager;

            public Handler(CatalogDbContext db, ICategoryManager manager)
            {
                _db = db;
                _manager = manager;
            }

            public async Task<ResponseViewModel<CategoryDto>> Handle(Command command,
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
                    var isParentExist = await _db.Categories.AnyAsync(
                        c => c.Id.ToLower() == command.ParentCategoryId.ToLower(),
                        cancellationToken: cancellationToken);
                    if (!isParentExist)
                    {
                        response.AddMessage("Invalid title.", MessageType.Error);
                        return await Task.FromResult(response);
                    }
                }

                var category = await _db.Categories.FindAsync(new []{command.Id}, cancellationToken);

                if (category is null)
                {
                    response.AddMessage("Invalid category.", MessageType.Error);
                    return await Task.FromResult(response);
                }

                category.Title = command.Title.Trim();
                category.Description = command.Description?.Trim();
                category.Slug =  await _manager.GenerateUniqueSlug(command.Slug ?? command.Title);
                category.DateUpdated = DateTime.UtcNow;
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