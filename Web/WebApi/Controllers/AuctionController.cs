using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Specifications;
using IAuctionService = WebApi.Interfaces.IAuctionService;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.ApiEndpoints.AuctionEndpoints;
using WebApi.Common;

namespace WebApi.Controllers
{
    // used abstract BaseController for such route with IAsyncRepository
    public class AuctionController : BaseController
    {
        private readonly IAsyncRepository<Auction> _auctionRepository;
        private readonly IAsyncRepository<Slot> _slotRepository;
        private readonly IAuctionService _auctionService;
        private readonly ILogger<Auction> _logger;
        // private readonly IMapper _mapper;

        public AuctionController(
            IAsyncRepository<Auction> auctionRepository,
            IAsyncRepository<Slot> slotRepository,
            IAuctionService auctionService,
            ILogger<Auction> logger,
            IMapper mapper) : base(mapper)
        {
            _auctionRepository = auctionRepository;
            _slotRepository = slotRepository;
            _auctionService = auctionService;
            _logger = logger;
            // _mapper = mapper;
        }
        
        [HttpGet]
        [SwaggerOperation(
            Summary = "Get list of auctions with specification",
            Description = "Return paginated collection of auctions that meet specification",
            OperationId = "auctions-list",
            Tags = new[] {"AuctionEndpoints"})]
        public async Task<ActionResult<ListPagedAuctionResponse>> ListPaged([FromQuery] ListPagedAuctionRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Call /api/Auction");

            // var category = request.CategoryId.HasValue
            //     ? await _categoryRepository.GetByIdAsync(request.CategoryId.Value, cancellationToken)
            //     : null;
            // var filterSpec = new AuctionFilterSpecification(request.CategoryId);
            // var totalItems = await _auctionRepository.CountAsync(filterSpec, cancellationToken);

            var includeSpec = new AuctionIncludeSpecification();
            var auctions = await _auctionRepository.ListAsync(includeSpec, cancellationToken);
            
            auctions = _auctionService.FilterAuctions(auctions, request.Title, request.StartTime, request.EndTime, request.CategoryId);
            
            var totalItems = auctions.Count;
            auctions = auctions
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();
            
            // var specPaged = new AuctionFilterPaginatedSpecification(
            //     (request.PageIndex-1) * request.PageSize, 
            //     request.PageSize    
            // );
            // var auctions = await _auctionRepository.ListAsync(specPaged, cancellationToken);

            var response = new ListPagedAuctionResponse(request.CorrelationId())
            {
                PageSize = request.PageSize,
                PageCount = int.Parse(Math.Ceiling((decimal) totalItems / request.PageSize).ToString()),
                PageIndex = request.PageIndex,
                Auctions = auctions.Select(a => Mapper.Map<Auction, AuctionDto>(a)).ToList(),
                ResponseItemsCount = totalItems
            };
            
            return Ok(response);
        }

        [HttpGet("{AuctionId}")]
        [SwaggerOperation(
            Summary = "Get auction by identification",
            Description = "Produce HTTPResponse of types: 200, 404",
            OperationId = "auctions-GetById",
            Tags = new[] {"AuctionEndpoints"})]
        public async Task<ActionResult<GetByIdAuctionResponse>> GetById([FromRoute] GetByIdAuctionRequest request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Call /api/Auction/{request.AuctionId}");

            var includeSpec = new AuctionIncludeSpecification(request.AuctionId);
            var auctions = await _auctionRepository.ListAsync(includeSpec, cancellationToken);
            if (auctions == null)
            {
                return NotFound();
            }

            var response = new GetByIdAuctionResponse(request.CorrelationId())
            {
                Auction = Mapper.Map<Auction, AuctionDto>(auctions.First())
            };

            return Ok(response);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Create new Auction",
            Description = "Paste form-data to new Auction instance",
            OperationId = "auctions-Create",
            Tags = new[] {"AuctionEndpoints"})]
        public async Task<ActionResult<CreateAuctionResponse>> Create([FromForm] CreateAuctionRequest request)
        {
            _logger.LogInformation("Call post on /api/Auction");
            
            var auctionModel = new Auction(request.Title, request.StartDate, request.EndDate);
            await _auctionService.AddSlotsToAuction(auctionModel, request.Slots);
            
            await _auctionRepository.AddAsync(auctionModel, new CancellationToken(false));
            
            var response = new CreateAuctionResponse(request.CorrelationId());
            response.Auction = Mapper.Map<Auction, AuctionDto>(auctionModel);

            return Ok(response);
        }

        [HttpPut("{AuctionId}")]
        [SwaggerOperation(
            Summary = "Update Auction",
            Description = "Update auction",
            OperationId = "auctions-Update",
            Tags = new[] {"AuctionEndpoints"})]
        public async Task<ActionResult<UpdateAuctionResponse>> Update(UpdateAuctionRequest request,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }
            
            //1. find item
            var auction = await _auctionRepository.GetByIdAsync(request.AuctionId, cancellationToken);
            if(auction != null)
            {
                //2. call updates on model
                auction.UpdateTitle(request.Title);
                auction.UpdatePeriod(request.StartedOn, request.EndedOn);

                var findSlots = request.Slots.Select(async s => await _slotRepository.GetByIdAsync(s, cancellationToken));
                var newSlots = new List<Slot>();
                foreach (var findSlot in findSlots)
                {
                    newSlots.Add(await findSlot);
                }
                auction.UpdateSlots(newSlots);
                //3. modify database entity
                await _auctionRepository.UpdateAsync(auction, cancellationToken);
                //4. create response model
                var response = new UpdateAuctionResponse(request.CorrelationId())
                {
                    Auction = Mapper.Map<Auction, AuctionDto>(auction)
                };
                //5. return response
                return response;
            }
            else
            {
                return NotFound();
            }
        }
        
        [HttpDelete("{AuctionId}")]
        [SwaggerOperation(
            Summary = "Delete auction",
            Description = "Use AuctionId to delete auction",
            OperationId = "auctions-Delete",
            Tags = new[] {"AuctionEndpoints"})]
        public async Task<ActionResult<DeleteAuctionResponse>> Delete([FromRoute] DeleteAuctionRequest request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Call delete on api/Auction/{request.AuctionId}");

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