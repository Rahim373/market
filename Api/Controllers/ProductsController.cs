using System.Threading.Tasks;
using Market.Application.Models;
using Market.Application.Products.Commands;
using Market.Application.Products.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Market.Api.Controllers
{
    public class ProductsController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseViewModel<ProductDto>>> GetProductAsync(string id)
        {
            return await Mediator.Send(new GetProduct(id));
        }

        [HttpGet("")]
        public async Task<ActionResult<ResponseViewModel<GridResponseViewModel<ProductDto>>>> GetProductsAsync(GetProductsWithPagination query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost("")]
        public async Task<ActionResult<ResponseViewModel<ProductDto>>> CreateProductAsync(
            [FromBody] AddProduct command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseViewModel<ProductDto>>> CreateProductAsync(string id,
            [FromBody] UpdateProduct command)
        {
            command.Id = id;
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseViewModel>> DeleteProductAsync(string id)
        {
            return await Mediator.Send(new DeleteProduct(id));
        }
    }
}