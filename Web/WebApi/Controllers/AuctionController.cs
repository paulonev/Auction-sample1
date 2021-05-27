using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.AuctionEndpoints;
using WebApi.Models;
using WebApi.Specifications;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // use abstract BaseController for such route with IAsyncRepository
    public class AuctionController : ControllerBase
    {
        private readonly IAsyncRepository<Auction> _auctionRepository;
        private readonly IAsyncRepository<Slot> _slotRepository;
        private readonly IAuctionService _auctionService;
        private readonly ILogger<Auction> _logger;
        private readonly IMapper _mapper;

        public AuctionController(
            IAsyncRepository<Auction> auctionRepository,
            IAsyncRepository<Slot> slotRepository,
            IAuctionService auctionService,
            ILogger<Auction> logger,
            IMapper mapper)
        {
            _auctionRepository = auctionRepository;
            _slotRepository = slotRepository;
            _auctionService = auctionService;
            _logger = logger;
            _mapper = mapper;
        }
        
        // GET /auctions
        [HttpGet("auctions")]
        // add swagger description
        public async Task<ActionResult<ListPagedAuctionResponse>> ListPaged([FromQuery] ListPagedAuctionRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Call /api/auctions");

            var filterSpec = new AuctionFilterSpecification(request.CategoryId);
            var totalItems = await _auctionRepository.CountAsync(filterSpec, cancellationToken);

            var specPaged = new AuctionFilterPaginatedSpecification(
                request.PageNumber * request.PageSize, 
                request.PageSize, 
                request.CategoryId);
            
            var auctions = await _auctionRepository.ListAsync(specPaged, cancellationToken);

            var response = new ListPagedAuctionResponse(request.CorrelationId());

            response.Auctions.AddRange(auctions.Select(_mapper.Map<AuctionDto>));
            response.PageCount = int.Parse(Math.Ceiling((decimal)totalItems / request.PageSize).ToString());

            return Ok(response);
        }

        [HttpGet("auctions/{AuctionId}")]
        public async Task<ActionResult<GetByIdAuctionResponse>> GetById([FromRoute] GetByIdAuctionRequest request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Call /api/auctions/{AuctionId}");

            var auction = await _auctionRepository.GetByIdAsync(request.AuctionId, cancellationToken);
            if (auction == null)
            {
                return NotFound();
            }
            
            var response = new GetByIdAuctionResponse(request.CorrelationId());
            response.Auction = new AuctionDto
            {
                Id = auction.Id,
                Title = auction.Title,
                EndedOn = auction.EndedOn,
                CategoryNames = await _auctionService.GetDistinctCategoryNames(auction.Id),
                AuctionSlotDtoItems = auction.Items.Select(_mapper.Map<AuctionSlotDto>).ToList()
            };

            return Ok(response);
        }

        [HttpPost("/auction-create")]
        //add swagger description
        public async Task<ActionResult<CreateAuctionResponse>> Create([FromForm] CreateAuctionRequest request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Call /api/auction-create");
            
            var auctionModel = new Auction(request.Title, request.StartDate, request.EndDate);
            foreach (var slotId in request.Slots)
            {
                var slot = await _slotRepository.GetByIdAsync(slotId);
                auctionModel.AddSlot(slot);
            }
            
            auctionModel = await _auctionRepository.AddAsync(auctionModel, cancellationToken);
            
            var response = new CreateAuctionResponse(request.CorrelationId());
            response.Auction = new AuctionDto
            {
                Id = auctionModel.Id,
                Title = auctionModel.Title,
                EndedOn = auctionModel.EndedOn,
                AuctionSlotDtoItems = auctionModel.Items.Select(_mapper.Map<AuctionSlotDto>).ToList(),
                CategoryNames = await _auctionService.GetDistinctCategoryNames(auctionModel.Id)
            };

            return Ok(response);
        }

        [HttpDelete("auctions/{AuctionId}")]
        public async Task<ActionResult<DeleteAuctionResponse>> Delete([FromRoute] DeleteAuctionRequest request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Call delete on /auctions/{AuctionId}");

            var auctionToDelete = await _auctionRepository.GetByIdAsync(request.AuctionId, cancellationToken);
            if (auctionToDelete == null)
            {
                return NotFound();
            }

            await _auctionRepository.DeleteAsync(auctionToDelete, cancellationToken);
            var response = new DeleteAuctionResponse(request.CorrelationId());
            
            return Ok(response);
        }

    }
}