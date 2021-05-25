using System.Linq;
using System.Threading.Tasks;
using ApiEndpointsProject.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.ViewModels.Auction;

namespace ApiEndpointsProject.Controllers
{
    public class AuctionController : Controller
    {
        private readonly IAuctionCatalogService _auctionCatalogService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ISlotViewModelService _slotService;
        
        public AuctionController(
            IAuctionCatalogService auctionCatalogService,
            SignInManager<ApplicationUser> signInManager,
            ISlotViewModelService slotService)
        {
            _auctionCatalogService = auctionCatalogService;
            _signInManager = signInManager;
            _slotService = slotService;
        }
        
        // public async Task<IActionResult> List(AuctionCatalogViewModel auctionCatalog, int? pageNumber, Guid? categoryId)
        // {
        //     auctionCatalog = await _auctionCatalogService.GetAuctions(pageNumber ?? 0, Constants.ItemsPerPage, auctionCatalog.CategoryApplied);
        //     
        //     return View(auctionCatalog);
        // }

        [HttpGet]
        [Authorize] //embed AuthorizeFilter in pipeline
        public async Task<IActionResult> Create()
        {
            //create auctionViewModel for authorized user
            var auctionStartup = await SetAuctionStartupAsync();

            return View(auctionStartup);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(AuctionViewModel auction)
        {
            //add auctions to list
        }

        private async Task<AuctionViewModel> SetAuctionStartupAsync()
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                var userSlots = await _slotService.GetUserSlots(HttpContext.User.Identity.Name);
                
                var vm = new AuctionViewModel();
                vm.Slots = userSlots.ToList();

                return vm;
            }
            else
            {
                // send user to login page
                
            }
        }
    }
}