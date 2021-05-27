using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Constants;
using ApplicationCore.Entities;
using Infrastructure.Data.DataAccess;
using Infrastructure.Identity;
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
        public static async Task SeedIdentityAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole("Administrator"));
            
            //create demo user
            var defaultUser = new ApplicationUser { UserName = "demouser", Email = "demouser@google.com" };
            await userManager.CreateAsync(defaultUser, AuthorizationConstants.DEFAULT_PASSWORD);

            //create admin
            string adminUserName = "admin007";
            var adminUser = new ApplicationUser { UserName = adminUserName, Email = "admin@google.com" };
            await userManager.CreateAsync(adminUser, AuthorizationConstants.DEFAULT_PASSWORD);
            
            adminUser = await userManager.FindByNameAsync(adminUserName);
            await userManager.AddToRoleAsync(adminUser, "Administrator");
        }
        
        public static async Task SeedAuctionAsync(AuctionDbContext auctionContext, ILoggerFactory logFactory, int retryTime = 0)
        {
            //Tables to seed: will have 2 slots, 1 auction, 6 bids, maybe 1 trade and 5 categories, 1 admin, 1 user
            try
            {
                if (!await auctionContext.Categories.AnyAsync())
                {
                    await auctionContext.Categories.AddRangeAsync(GetConfiguredCategories());

                    await auctionContext.SaveChangesAsync();
                }
                
                if (!await auctionContext.Slots.AnyAsync())
                {
                    await auctionContext.Slots.AddRangeAsync(GetConfiguredSlots());
                
                    await auctionContext.SaveChangesAsync();
                }

                // if (!await auctionContext.Bids.AnyAsync())
                // {
                //     await auctionContext.Bids.AddRangeAsync(GetConfiguredBids());f
                //     
                //     await auctionContext.SaveChangesAsync();
                // }
            }
            catch (Exception ex)
            {
                if (retryTime < 5)
                {
                    retryTime++;
                    var log = logFactory.CreateLogger<AuctionDbContext>();
                    log.LogError(ex.Message);
                    await SeedAuctionAsync(auctionContext, logFactory, retryTime);
                }
                throw;
            }
        }

        static IEnumerable<Category> GetConfiguredCategories()
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

        static IEnumerable<Slot> GetConfiguredSlots()
        {
            var firstSlot = new Slot("T-Shirt V-neck", "For men 170cm tall", new Guid("76eb9a11-b63d-40d2-b256-7feca943275a"), Guid.NewGuid(), 15m, new List<Picture>() {new Picture() {Url = "https://cdn5.vectorstock.com/i/1000x1000/50/04/broken-glass-icon-vector-10905004.jpg"}});
            var secondSlot = new Slot("T-Shirt S-neck", "For men 156cm tall", new Guid("76eb9a11-b63d-40d2-b256-7feca943275a"), Guid.NewGuid(), 10m, new List<Picture>() {new Picture() {Url = "https://cdn5.vectorstock.com/i/1000x1000/50/04/broken-glass-icon-vector-10905004.jpg"}});
            var thirdSlot = new Slot("T-Shirt BlackStarWear", "For men 180cm tall", new Guid("76eb9a11-b63d-40d2-b256-7feca943275a"), Guid.NewGuid(), 35m, new List<Picture>() {new Picture() {Url = "https://cdn5.vectorstock.com/i/1000x1000/50/04/broken-glass-icon-vector-10905004.jpg"}});

            return new List<Slot> {firstSlot, secondSlot, thirdSlot};
        }
    }
}