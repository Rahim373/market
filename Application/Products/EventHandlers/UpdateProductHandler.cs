using System;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using Market.Application.Interfaces;
using Market.Application.Models;
using Market.Application.Products.Commands;
using Market.Application.Products.Interfaces;

namespace Market.Applications.Products.Categories
{
    public class Edit
    {
        public class UpdateProductHandler : IHandlerWrapper<UpdateProduct, ProductDto>
        {
            private readonly IApplicationDbContext _db;
            private readonly IProductManager _manager;

            public UpdateProductHandler(IApplicationDbContext db, IProductManager manager)
            {
                _db = db;
                _manager = manager;
            }

            public async Task<ResponseViewModel<ProductDto>> Handle(UpdateProduct command,
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
                product.Slug = await _manager.GenerateUniqueSlug(command.Slug ?? command.Title, null, cancellationToken);
                product.Active = command.Active;

                await _db.SaveChangesAsync(cancellationToken);

                response.Entity = product.Adapt<ProductDto>();
                response.Succeed();
                response.AddMessage("Successfully updated.", MessageType.Success);

                return await Task.FromResult(response);
            }
        }
    }
}