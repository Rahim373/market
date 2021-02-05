using System.Threading.Tasks;
using Market.Application.Categories.Commands;
using Market.Application.Categories.Queries;
using Market.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Market.Api.Controllers
{
    public class CategoriesController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseViewModel<CategoryDto>>> GetCategoryAsync(string id)
        {
            return await Mediator.Send(new GetCategory(id));
        }

        [HttpGet("")]
        public async Task<ActionResult<ResponseViewModel<GridResponseViewModel<CategoryDto>>>> GetCategoriesAsync([FromQuery]GetCategoriesWithPagination query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost("")]
        public async Task<ActionResult<ResponseViewModel<CategoryDto>>> CreateCategoryAsync(
            [FromBody] AddCategory command)
        {
            return await Mediator.Send(command);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseViewModel<CategoryDto>>> CreateCategoryAsync(string id,
            [FromBody]UpdateCategory command)
        {
            command.Id = id;
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseViewModel>> DeleteCategoryAsync(string id)
        {
            return await Mediator.Send(new DeleteCategory(id));
        }
    }
}