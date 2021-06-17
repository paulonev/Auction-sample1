using WebApi.Common;

namespace WebApi.AuthApi.Endpoints
{
    public class CreateUserRequest : BaseRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}