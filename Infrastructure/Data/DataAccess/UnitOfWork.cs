using System.Threading.Tasks;
using ApplicationCore.Interfaces;

namespace Infrastructure.Data.DataAccess
{
    public class UnitOfWork
    {
        private readonly AuctionDbContext _dbContext;
    
        public UnitOfWork(AuctionDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    
        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}