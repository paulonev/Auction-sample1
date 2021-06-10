using System;
using WebApi.Common;

namespace WebApi.AuthApi.Endpoints
{
    public class CreateUserResponse : BaseResponse
    {
        public CreateUserResponse()
        {
        }
        
        public CreateUserResponse(Guid correlationId) : base(correlationId)
        {
        }
        
        
    }
}