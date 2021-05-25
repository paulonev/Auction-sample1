using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEndpointsProject.Interfaces;
using ApiEndpointsProject.Specifications;
using ApiEndpointsProject.ViewModels.Auction;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using WebApi.ViewModels.Auction;

namespace ApiEndpointsProject.Services
{
    /// <summary>
    /// UI-specific service
    /// Potential issue - methods should return DTOs, provide paginated response(somehow via filter)
    /// </summary>
    public class AuctionCatalogService : IAuctionCatalogService
    {
        private readonly ILogger<AuctionCatalogService> _logger;
        private IAsyncRepository<Auction> _auctionRepository { get; set; }
        private IAsyncRepository<Category> _categoryRepository { get; set; }
        
        // may add dependencies in future
        public AuctionCatalogService(
            ILogger<AuctionCatalogService> logger,
            IAsyncRepository<Auction> auctionRepository,
            IAsyncRepository<Category> categoryRepository)
        {
            _logger = logger;
            _auctionRepository = auctionRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<AuctionCatalogViewModel> GetAuctions(int pageNumber, int pageSize, Guid? categoryId)
        {
            _logger.LogInformation("GetAuctions called.");
            
            var filterPaginatedSpecification =
                new AuctionCatalogFilterPaginatedSpecification(pageSize * pageNumber, pageSize, categoryId);
            var filterSpecification = 
                new AuctionCatalogFilterSpecification(categoryId);

            var slotsOnPage = await _auctionRepository.ListAsync(filterPaginatedSpecification);
            var totalItems = await _auctionRepository.CountAsync(filterSpecification);

            var slotCatalogViewModel = new AuctionCatalogViewModel()
            {
                Slots = slotsOnPage.Select(s => new AuctionViewModel()
                {
                    Id = s.Id,
                    Title = s.Title,
                    Description = s.Description,
                    CreatorName = s.Owner.NickName,
                    Pictures = s.Pictures, /*how to work with pics*/
                    StartPrice = s.StartPrice,
                    Status = s.Status.ToString()
                }).ToList(),
                Categories = (await GetCategories()).ToList(),
                PaginationInfo = new PaginationInfoViewModel()
                {
                    CurrentPage = pageNumber,
                    ItemsPerPage = pageSize,
                    TotalItems = totalItems
                }
            };

            slotCatalogViewModel.PaginationInfo.Next =
                (slotCatalogViewModel.PaginationInfo.CurrentPage == slotCatalogViewModel.PaginationInfo.TotalPages - 1)
                    ? "is-disabled"
                    : "";
            slotCatalogViewModel.PaginationInfo.Previous =
                (slotCatalogViewModel.PaginationInfo.CurrentPage == 0) ? "is-disabled" : "";

            return slotCatalogViewModel;
        }
        
        public Task<IEnumerable<Slot>> GetTodaySlots()
        {
            throw new NotImplementedException();
        }
        
        private async Task<List<SelectListItem>> GetCategories()
        {
            _logger.LogInformation("GetCategories called.");

            var categories = await _categoryRepository.ListAllAsync();

            var items = categories
                .Select(c => new SelectListItem {Value = c.Id.ToString(), Text = c.Name})
                .OrderBy(i => i.Text)
                .ToList();

            var allItem = new SelectListItem() {Value = null, Text = "All", Selected = true};
            items.Insert(0, allItem);

            return items;
        }
    }
}