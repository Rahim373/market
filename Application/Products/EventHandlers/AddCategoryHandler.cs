using Mapster;
using System;
using System.Threading;
using System.Threading.Tasks;
using Market.Application.Interfaces;
using Market.Application.Models;
using Market.Application.Products.Commands;
using Market.Application.Products.Interfaces;
using Market.Domain.Entities;

namespace Market.Application.Products.EventHandlers
{
    public class AddProductHandler : IHandlerWrapper<AddProduct, ProductDto>
    {
        private readonly IApplicationDbContext _db;
        private readonly IProductManager _manager;

        public AddProductHandler(IApplicationDbContext db, IProductManager manager)
        {
            _db = db;
            _manager = manager;
        }

        public async Task<ResponseViewModel<ProductDto>> Handle(AddProduct command,
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
            product.Slug = await _manager.GenerateUniqueSlug(command.Slug ?? command.Title, null, cancellationToken);
            await _db.Products.AddAsync(product, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);

            response.Entity = product.Adapt<ProductDto>();
            response.Succeed();
            response.AddMessage("Successfully saved.", MessageType.Success);

            return await Task.FromResult(response);
        }
    }
}