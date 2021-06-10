using WebApi.Common;

namespace WebApi.AuthApi.Endpoints
{
    public class LoginRequest : BaseRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}