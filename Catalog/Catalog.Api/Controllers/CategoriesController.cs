using Market.Catalog.Applications.Categories.Cqrs;
using Market.Catalog.Applications.Categories.Dtos;
using Market.Common;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Market.Catalog.Api.Controllers
{
    public class CategoriesController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseViewModel<CategoryDto>>> GetCategoryAsync(string id)
        {
            return await Mediator.Send(new Get.Query(id));
        }

        [HttpGet("")]
        public async Task<ActionResult<ResponseViewModel<GridResponseViewModel<CategoryDto>>>> GetCategoriesAsync()
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpPost("")]
        public async Task<ActionResult<ResponseViewModel<CategoryDto>>> CreateCategoryAsync(
            [FromBody] Create.Command command)
        {
            return await Mediator.Send(command);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseViewModel<CategoryDto>>> CreateCategoryAsync(string id,
            [FromBody] Edit.Command command)
        {
            command.Id = id;
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseViewModel>> DeleteCategoryAsync(string id)
        {
            return await Mediator.Send(new Delete.Command(id));
        }
    }
}