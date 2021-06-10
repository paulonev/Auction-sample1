using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.AppSettings;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SettingsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public SettingsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        [HttpGet]
        [SwaggerOperation(
            Summary = "Get Auth0 credentials",
            Description = "Get Auth0 credentials",
            OperationId = "settings",
            Tags = new[] {"SettingsEndpoints"})]
        public ActionResult<Auth0Settings> GetAuth0Settings()
        {
            try
            {
                var dto = new Auth0Settings()
                {
                    Audience = _configuration.GetValue<string>("Auth:Audience"),
                    Domain = _configuration.GetValue<string>("Auth:Domain"),
                    ClientId = _configuration.GetValue<string>("Auth:ClientId")
                };
                return dto;
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}