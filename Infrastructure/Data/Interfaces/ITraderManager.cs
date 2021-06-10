using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data.Interfaces
{
    public interface ITraderManager
    {
        Task<Trader> GetTraderById(Guid id);
        Task<IdentityResult> CreateTrader(string email, string password);
        Task<IdentityResult> CreateTrader(ApplicationUser user, string password);
        Task<(Result result, Guid userId)> SignIn(string email, string password);
        Task CreateRole(IdentityRole role);
        Task<IdentityResult> AddToRole(ApplicationUser user, string role);
        Task<Result> AddToRole(string email, string role, Guid currentUserId);
        Task<IList<string>> GetUserRoles(Guid userId);
        Task RemoveFromRole(string username, string role, string currentUserId, CancellationToken cancellationToken);
    }
}