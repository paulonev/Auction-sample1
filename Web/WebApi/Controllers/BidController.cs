using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.ApiEndpoints.BidEndpoints;
using WebApi.Common;
using WebApi.Specifications;

namespace WebApi.Controllers
{
    public class BidController : BaseController
    {
        private readonly IAsyncRepository<Bid> _bidRepository;
        
        public BidController(
            IAsyncRepository<Bid> bidRepository,
            IMapper mapper) : base(mapper)
        {
            _bidRepository = bidRepository;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Get all bids of particular slot",
            Description = "Get all bids of particular slot",
            OperationId = "bid-GetBySlotId",
            Tags = new[] {"BidEndpoints"})]
        public async Task<ActionResult<GetBidsBySlotIdResponse>> Get([FromQuery] GetBidsBySlotIdRequest request,
            CancellationToken cancellationToken)
        {
            var bidsSpecification = new BidSpecification(request.SlotId);
            var bids = await _bidRepository.ListAsync(bidsSpecification, cancellationToken);
            
            var response = new GetBidsBySlotIdResponse(request.CorrelationId());
            var responseBids = bids.Select(Mapper.Map<BidDto>).ToList();

            response.Bids = responseBids;
            response.ItemsFetched = responseBids.Count;
            
            return Ok(response);
        }

        [HttpGet]
        [Route(nameof(LatestBid))]
        [SwaggerOperation(
            Summary = "Get winning bid of particular slot",
            Description = "Get winning bid of particular slot",
            OperationId = "bid-GetBySlotId",
            Tags = new[] {"BidEndpoints"})]
        public async Task<ActionResult<SlotLatestBidResponse>> LatestBid([FromQuery] SlotLatestBidRequest request,
            CancellationToken cancellationToken)
        {
            var bidsSpecification = new BidSpecification(request.SlotId, true);
            var highestBid = await _bidRepository.LastOfDefaultAsync(bidsSpecification, cancellationToken);

            var response = new SlotLatestBidResponse(request.CorrelationId());
            response.Bid = Mapper.Map<Bid, BidDto>(highestBid);

            return Ok(response);
        }
        
        [Authorize, HttpPost]
        [SwaggerOperation(
            Summary = "Create new Bid",
            Description = "Create new Bid",
            OperationId = "bid-Create",
            Tags = new[] {"BidEndpoints"})]
        public async Task<ActionResult<CreateBidResponse>> Post([FromForm] CreateBidRequest request,
            CancellationToken cancellationToken)
        {
            var bid = new Bid(request.TraderId, request.SlotId, request.Amount, request.Date);
            // append layer in infrastructure for bidding (generate idea)
            await _bidRepository.AddAsync(bid, cancellationToken);
            
            var bidsSpecification = new BidSpecification(request.SlotId);
            var bids = await _bidRepository.ListAsync(bidsSpecification, cancellationToken);
            
            var response = new CreateBidResponse(request.CorrelationId());
            response.Bids = bids.Select(Mapper.Map<BidDto>).ToList();

            return Ok(response);
        }
    }
}