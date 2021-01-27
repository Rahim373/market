using Mapster;
using Market.Catalog.Applications.Products.Dtos;
using Market.Catalog.Applications.Products.Manager;
using Market.Catalog.Domain.Context;
using Market.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Market.Catalog.Applications.Products.Cqrs
{
    public class Edit
    {
        public class Command : IRequestWrapper<ProductDto>
        {
            public string Id { get; set; }
            public string ItemCode { get; set; }
            public string Title { get; set; }
            public string Slug { get; set; }
            public string Description { get; set; }
            public string ExtendedDescription { get; set; }
            public bool Active { get; set; }
            public string CategoryId { get; set; }
        }

        public class Handler : IHandlerWrapper<Command, ProductDto>
        {
            private readonly CatalogDbContext _db;
            private readonly IProductManager _manager;

            public Handler(CatalogDbContext db, IProductManager manager)
            {
                _db = db;
                _manager = manager;
            }

            public async Task<ResponseViewModel<ProductDto>> Handle(Command command,
                CancellationToken cancellationToken)
            {
                var response = new ResponseViewModel<ProductDto>();

                if (string.IsNullOrEmpty(command.Title))
                {
                    response.AddMessage("Invalid title.", MessageType.Error);
                    return await Task.FromResult(response);
                }

                if (!string.IsNullOrEmpty(command.CategoryId))
                {
                    var isCategoryExist = await _manager.IsCategoryExistsAsync(command.CategoryId, cancellationToken);

                    if (!isCategoryExist)
                    {
                        response.AddMessage("Invalid category.", MessageType.Error);
                        return await Task.FromResult(response);
                    }
                }
                else
                {
                    response.AddMessage("Undefined category.", MessageType.Error);
                    return await Task.FromResult(response);
                }

                var product = await _db.Products.FindAsync(new[] { command.Id }, cancellationToken);

                if (product is null)
                {
                    response.AddMessage("Invalid product.", MessageType.Error);
                    return await Task.FromResult(response);
                }

                product.ItemCode = command.ItemCode?.Trim();
                product.Title = command.Title.Trim();
                product.Description = command.Description?.Trim();
                product.ExtendedDescription = command.ExtendedDescription?.Trim();
                product.Slug = await _manager.GenerateUniqueSlug(command.Slug ?? command.Title);
                product.Active = command.Active;
                product.DateUpdated = DateTime.UtcNow;

                await _db.SaveChangesAsync(cancellationToken);

                response.Entity = product.Adapt<ProductDto>();
                response.Succeed();
                response.AddMessage("Successfully updated.", MessageType.Success);

                return await Task.FromResult(response);
            }
        }
    }
}