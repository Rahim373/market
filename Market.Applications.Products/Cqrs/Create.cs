using Mapster;
using Market.Applications.Products.Manager;
using System;
using System.Threading;
using System.Threading.Tasks;
using Market.Applications.Products.Dtos;
using Market.Common;
using Market.Domain.Context;
using Market.Domain.Models;

namespace Market.Applications.Products.Cqrs
{
    public class Create
    {
        public class Command : IRequestWrapper<ProductDto>
        {
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
            private readonly MarketDbContext _db;
            private readonly IProductManager _manager;

            public Handler(MarketDbContext db, IProductManager manager)
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
                    response.AddMessage("Invalid title", MessageType.Error);
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

                var product = command.Adapt<Product>();
                product.ItemCode = product.ItemCode?.Trim();
                product.Title = product.Title.Trim();
                product.Description = product.Description?.Trim();
                product.ExtendedDescription = product.ExtendedDescription?.Trim();
                product.Slug = await _manager.GenerateUniqueSlug(command.Slug ?? command.Title);
                product.DateCreated = DateTime.UtcNow;
                product.DateUpdated = DateTime.UtcNow;
                await _db.Products.AddAsync(product, cancellationToken);
                await _db.SaveChangesAsync(cancellationToken);

                response.Entity = product.Adapt<ProductDto>();
                response.Succeed();
                response.AddMessage("Successfully saved.", MessageType.Success);

                return await Task.FromResult(response);
            }
        }
    }
}