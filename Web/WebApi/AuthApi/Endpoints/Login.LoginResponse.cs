using System;
using WebApi.Common;

namespace WebApi.AuthApi.Endpoints
{
    public class LoginResponse : BaseResponse
    {
        public LoginResponse(Guid correlationId) : base(correlationId)
        {
        }

        public LoginResponse()
        {
        }
        
        //tokens
        public string Token { get; set; }
    }
}