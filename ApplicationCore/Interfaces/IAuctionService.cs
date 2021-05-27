using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IAuctionService
    {
        Task AddSlotToAuction(Slot slot);
        Task<List<string>> GetDistinctCategoryNames(Guid auctionId);
    }
}