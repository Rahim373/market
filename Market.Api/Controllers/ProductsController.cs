using System.Threading.Tasks;
using Market.Applications.Products.Cqrs;
using Market.Applications.Products.Dtos;
using Market.Common;
using Microsoft.AspNetCore.Mvc;

namespace Market.Api.Controllers
{
    public class ProductsController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseViewModel<ProductDto>>> GetProductAsync(string id)
        {
            return await Mediator.Send(new Get.Query(id));
        }

        [HttpGet("")]
        public async Task<ActionResult<ResponseViewModel<GridResponseViewModel<ProductDto>>>> GetProductsAsync()
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpPost("")]
        public async Task<ActionResult<ResponseViewModel<ProductDto>>> CreateProductAsync(
            [FromBody] Create.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseViewModel<ProductDto>>> CreateProductAsync(string id,
            [FromBody] Edit.Command command)
        {
            command.Id = id;
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseViewModel>> DeleteProductAsync(string id)
        {
            return await Mediator.Send(new Delete.Command(id));
        }
    }
}