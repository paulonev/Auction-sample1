using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ITokenClaimsService
    {
        Task<string> GetTokenAsync(string userName);
        Task<string> GetEncryptedToken(string userName, string userId);
    }
}