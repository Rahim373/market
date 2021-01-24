using System;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using Market.Catalog.Applications.Categories.Dtos;
using Market.Catalog.Applications.Categories.Manager;
using Market.Catalog.Domain.Context;
using Market.Catalog.Domain.Models;
using Market.Common;
using Microsoft.EntityFrameworkCore;

namespace Market.Catalog.Applications.Categories.Cqrs
{
    public class Create
    {
        public class Command : IRequestWrapper<CategoryDto>
        {
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
                category.Slug = await _manager.GenerateUniqueSlug(command.Slug ?? command.Title);
                category.DateCreated = DateTime.UtcNow;
                category.DateUpdated = DateTime.UtcNow;
                await _db.Categories.AddAsync(category, cancellationToken);
                await _db.SaveChangesAsync(cancellationToken);

                response.Entity = category.Adapt<CategoryDto>();
                response.Succeed();
                response.AddMessage("Successfully saved.", MessageType.Success);

                return await Task.FromResult(response);
            }
        }
    }
}