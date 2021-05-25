using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using Infrastructure.Common;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.ApiController
{
    public class UserController : Controller
    {
        private readonly ITokenClaimsService _tokenClaimsService;

        public UserController(ITokenClaimsService tokenClaimsService)
        {
            _tokenClaimsService = tokenClaimsService;
        }
        
        [HttpGet]
        [Authorize]
        [AllowAnonymous]
        public async Task<IActionResult> GetCurrentUser() =>
            Ok(User.Identity.IsAuthenticated ? await CreateUserInfo(User) : UserInfo.Anonymous);

        // compare with UserLoginInfo
        private async Task<UserInfo> CreateUserInfo(ClaimsPrincipal user)
        {
            if (!user.Identity.IsAuthenticated)
            {
                return UserInfo.Anonymous;
            }

            var userInfo = new UserInfo()
            {
                IsAuthenticated = true
            };
            
            if (user.Identity is ClaimsIdentity claimsIdentity)
            {
                userInfo.NameClaimType = claimsIdentity.NameClaimType;
                userInfo.RoleClaimType = claimsIdentity.RoleClaimType;
            }
            else
            {
                userInfo.NameClaimType = "name";
                userInfo.RoleClaimType = "role";
            }
            
            if (user.Claims.Any())
            {
                var claims = new List<ClaimValue>();
                var nameClaims = user.FindAll(userInfo.NameClaimType);
                foreach (var claim in nameClaims)
                {
                    claims.Add(new ClaimValue(userInfo.NameClaimType, claim.Value));
                }

                foreach (var claim in user.Claims.Except(nameClaims))
                {
                    claims.Add(new ClaimValue(claim.Type, claim.Value));
                }

                userInfo.Claims = claims;
            }

            var token = await _tokenClaimsService.GetTokenAsync(user.Identity?.Name);
            userInfo.Token = token;
            
            return userInfo;
        }
    }
}    