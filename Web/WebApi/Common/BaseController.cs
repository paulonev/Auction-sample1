using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Common
{
    [ApiController]
    [Route("api/[controller]")]
    //[Produces("application/json")]
    public abstract class BaseController : ControllerBase
    {
        protected readonly IMapper Mapper;

        protected BaseController(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}