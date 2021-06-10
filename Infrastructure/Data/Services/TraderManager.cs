using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using Infrastructure.Data.Identity;
using Infrastructure.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Services
{
    public class TraderManager : ITraderManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AuctionDbContext _dbContext;

        public TraderManager(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            AuctionDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }
        
        public async Task<Trader> GetTraderById(Guid id)
        {
            var result = await _dbContext
                .Users
                .Where(u => u.Id == id.ToString())
                .SingleOrDefaultAsync();

            if (result == null)
            {
                return null;
            }

            var user = new ApplicationUser()
            {
                Id = result.Id,
                Email = result.Email,
                UserName = result.UserName,
                AccessFailedCount = result.AccessFailedCount,
                LockoutEnd = result.LockoutEnd,
                PhoneNumber = result.PhoneNumber,
                PhoneNumberConfirmed = result.PhoneNumberConfirmed,
                TwoFactorEnabled = result.TwoFactorEnabled
            };

            var trader = new Trader()
            {
                IdentityGuid = Guid.Parse(user.Id)
            };
            
            return trader;
        }

        public async Task<IdentityResult> CreateTrader(string email, string password)
        {
            var user = new ApplicationUser()
            {
                UserName = email,
                Email = email
            };
            var result = await _userManager.CreateAsync(user, password);

            var trader = new Trader()
            {
                IdentityGuid = Guid.Parse(await _userManager.GetUserIdAsync(user))
            };
            await _dbContext.Traders.AddAsync(trader);
            await _dbContext.SaveChangesAsync();
            
            return result;
        }

        public async Task<IdentityResult> CreateTrader(ApplicationUser user, string password)
        { 
            var result = await _userManager.CreateAsync(user, password);

            var trader = new Trader()
            {
                IdentityGuid = Guid.Parse(await _userManager.GetUserIdAsync(user))
            };
            await _dbContext.Traders.AddAsync(trader);
            await _dbContext.SaveChangesAsync();
            
            return result;
        }

        public async Task<(Result result, Guid userId)> SignIn(string email, string password)
        {
            var user = await _dbContext.Users
                .Where(u => u.Email == email)
                .SingleOrDefaultAsync();

            if (user == null)
            {
                return (Result.Failure("User not found. Try again with valid email and password."), Guid.Empty);
            }
            
            if (await _userManager.IsLockedOutAsync(user))
            {
                return (Result.Failure("Account has been locked out due to many failed log-in attempts"), Guid.Empty);
            }

            var validPass = await _userManager.CheckPasswordAsync(user, password);
            if (!validPass)
            {
                await _userManager.AccessFailedAsync(user);
                return (Result.Failure("Invalid password."), Guid.Empty);
            }
            
            //email confirmation

            return (Result.Success(), Guid.Parse(user.Id));
        }

        
        // not implemented
        public Task CreateRole(IdentityRole role)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> AddToRole(ApplicationUser user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<Result> AddToRole(string email, string role, Guid currentUserId)
        {
            var user = await _dbContext.Users
                .Where(u => u.Email == email)
                .SingleOrDefaultAsync();

            if (user == null)
            {
                return Result.Failure($"Failed to add user to role {role} due to invalid credentials");
            }
            
            var result = await _userManager.AddToRoleAsync(user, role);
            return result.Succeeded
                ? Result.Success()
                : Result.Failure($"Failed to add user to role {role} due to invalid credentials");
        }

        public Task<IList<string>> GetUserRoles(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRole(string username, string role, string currentUserId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}