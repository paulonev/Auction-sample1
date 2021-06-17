using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Constants;
using ApplicationCore.Entities;
using Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    /// <summary>
    /// Seeding auction context
    /// </summary>
    public class AuctionContextSeed
    {
        private readonly AuctionDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuctionContextSeed(
            AuctionDbContext context, 
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        
        public async Task SeedIdentityAsync(ILoggerFactory logFactory)
        {
            var log = logFactory.CreateLogger<AuctionDbContext>();
            log.Log(LogLevel.Information, "Seeding identity context to the database");
            
            await _roleManager.CreateAsync(new IdentityRole("Administrator")); // extract to app constant
            
            //create demo user
            var defaultUser = new ApplicationUser { UserName = "demouser", Email = "demouser@google.com" };
            await _userManager.CreateAsync(defaultUser, AuthorizationConstants.DEFAULT_PASSWORD);

            //create admin
            string adminUserName = "admin001";
            var adminUser = new ApplicationUser { UserName = adminUserName, Email = "admin@google.com" };
            await _userManager.CreateAsync(adminUser, AuthorizationConstants.DEFAULT_PASSWORD);
            
            adminUser = await _userManager.FindByNameAsync(adminUserName);
            await _userManager.AddToRoleAsync(adminUser, "Administrator");
        }
        
        public async Task SeedAuctionAsync(ILoggerFactory logFactory, int retryTime = 0)
        {
            //Tables to seed: will have 2 slots, 1 auction, 6 bids
            try
            {
                // _context.Slots.RemoveRange(_context.Slots);
                // _context.Categories.RemoveRange(_context.Categories);
                // _context.Traders.RemoveRange(_context.Traders);
                await _context.SaveChangesAsync();
                
                if (!await _context.Categories.AnyAsync())
                {
                    await _context.Categories.AddRangeAsync(GetConfiguredCategories());

                    await _context.SaveChangesAsync();
                }
                
                if (!await _context.Traders.AnyAsync())
                {
                    await _context.Traders.AddRangeAsync(GetConfiguredTraders());

                    await _context.SaveChangesAsync();
                }
                
                if (!await _context.Slots.AnyAsync())
                {
                    await _context.Slots.AddRangeAsync(GetConfiguredSlots());
                
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryTime < 5)
                {
                    retryTime++;
                    var log = logFactory.CreateLogger<AuctionDbContext>();
                    log.LogError(ex.Message);
                    await SeedAuctionAsync(logFactory, retryTime);
                }
                throw;
            }
        }

        private IEnumerable<Trader> GetConfiguredTraders()
        {
            var traders = new List<Trader>();
            foreach (var user in _context.Users)
            {
                var trader = new Trader() {IdentityGuid = Guid.Parse(user.Id)};
                traders.Add(trader);
            }

            return traders;
        }

        private IEnumerable<Category> GetConfiguredCategories()
        {
            var c1Id = "76eb9a11-b63d-40d2-b256-7feca943275a";
            var c1 = new Category(new Guid(c1Id), "c1");
            
            var c1SubCategories = new List<Category>()
            {
                new Category(Guid.NewGuid(), "c1sub1", c1),
                new Category(Guid.NewGuid(), "c1sub2", c1),
                new Category(Guid.NewGuid(), "c1sub3", c1),
                new Category(Guid.NewGuid(), "c1sub4", c1)
            };

            c1.AddCategories(c1SubCategories);
            
            var c2 = new Category(new Guid("63f0c87c-b3a0-4cfe-b8d4-1f51d263fea8"), "c2");
            
            var c2SubCategories = new List<Category>()
            {
                new Category(Guid.NewGuid(), "c2sub1", c2),
                new Category(Guid.NewGuid(), "c2sub2", c2)
            };

            c2.AddCategories(c2SubCategories);
            
            var categories = new List<Category>();
            categories.Add(c1);
            categories.AddRange(c1SubCategories);
            categories.Add(c2);
            categories.AddRange(c2SubCategories);
            return categories;
        }

        private IEnumerable<Slot> GetConfiguredSlots()
        {
            var slotOwnerId = Guid.Parse(_context.Users.First().Id);
            var slotCategoryId = _context.Categories.First().Id;
            
            var firstSlot = new Slot(
                "T-Shirt V-neck",
                "For men 170cm tall",
                slotCategoryId,
                slotOwnerId,
                null,
                15m
            );
            var secondSlot = new Slot(
                "T-Shirt S-neck",
                "For men 156cm tall",
                slotCategoryId,
                slotOwnerId,
                null,
                15m
            );
            var thirdSlot = new Slot(
                "T-Shirt BlackStarWear",
                "For men 180cm tall",
                slotCategoryId,
                slotOwnerId,
                null,
                15m
            );
            
            var allItems = new List<Slot> {firstSlot, secondSlot, thirdSlot};
            return allItems;
        }
    }
}