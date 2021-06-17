using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.ApiEndpoints.CategoryEndpoints;
using WebApi.Common;

namespace WebApi.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly IAsyncRepository<Category> _categoryRepository;
        
        public CategoryController(
            IAsyncRepository<Category> categoryRepository,
            IMapper mapper) : base(mapper)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Get all categories",
            Description = "Get all categories",
            OperationId = "categories-list",
            Tags = new[] {"CategoryEndpoints"})]
        public async Task<ActionResult<ListCategoriesResponse>> List([FromRoute] ListCategoriesRequest request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.ListAllAsync(cancellationToken);
            
            var response = new ListCategoriesResponse(request.CorrelationId());
            response.Categories = categories.Select(Mapper.Map<CategoryDto>).ToList();
            return Ok(response);
        }
    }
}