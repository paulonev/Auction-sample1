using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using Infrastructure.Data.Interfaces;
using Infrastructure.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.AuthApi.Endpoints;

namespace WebApi.AuthApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        //signInManager
        private readonly ITraderManager _traderManager;
        //emailSender

        //tokenClaimsService
        private readonly ITokenClaimsService _tokenClaims;
        
        public AuthController(
            ITraderManager traderManager,
            ITokenClaimsService tokenClaims)
        {
            _traderManager = traderManager;
            _tokenClaims = tokenClaims;
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Login a user",
            Description = "Login a user providing new accessToken(jwt)",
            OperationId = "auth-login",
            Tags = new[] {"AuthEndpoints"})]
        [SwaggerResponse(
            StatusCodes.Status200OK,
            "User logged in with accessToken",
            typeof(LoginResponse)
        )]
        [SwaggerResponse(
            StatusCodes.Status400BadRequest,
            "Failed to login user",
            typeof(TraderManager)
        )]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request, CancellationToken cancellationToken)
        {
            //1.sign user in with response of user id
            var (result, userId) = await _traderManager.SignIn(request.Email, request.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Error);
            }
            //2.generates auth tokens
            //3.add tokens to response, set cookie
            var response = new LoginResponse()
            {
                Token = await _tokenClaims.GetEncryptedToken(request.Email, userId.ToString())
            };
            
            SetJwtCookie(response.Token);
            
            //4.return response
            return Ok(response);
        }
        
        [HttpPost]
        [SwaggerOperation(
            Summary = "Register new user",
            Description = "Register new user",
            OperationId = "auth-register",
            Tags = new[] {"AuthEndpoints"})]
        public async Task<ActionResult<CreateUserResponse>> Register(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var result = await _traderManager.CreateTrader(request.Email, request.Password);
            if (!result.Succeeded)
            {
                return BadRequest(new Exception(nameof(_traderManager.CreateTrader) + "User creation exception"));
            }
            
            //email confirmation code generation and sendEmail
            
            var response = new CreateUserResponse();
            return Ok(response);
        }
        
        [HttpPost]
        [SwaggerOperation(
            Summary = "Logout a user",
            Description = "Logout a user by deleting accessToken(jwt) from browser cookies",
            OperationId = "auth-logout",
            Tags = new[] {"AuthEndpoints"})]
        public async Task<ActionResult<LogoutResponse>> Logout(LogoutRequest request, CancellationToken cancellationToken)
        {
            //invalidate refresh token
            Response.Cookies.Delete("accessToken");
            
            var response = new LogoutResponse();
            return Ok(response);
        }
        
        
        private void SetJwtCookie(string token)
        {
            var cookieOptions = new CookieOptions()
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTimeOffset.MaxValue
            };
            
            Response.Cookies.Append("accessToken", token, cookieOptions);
        }
    }
}