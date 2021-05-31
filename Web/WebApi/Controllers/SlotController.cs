using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.ApiEndpoints.SlotEndpoints;
using WebApi.Common;
using WebApi.Interfaces;

namespace WebApi.Controllers
{
    public class SlotController : BaseController
    {
        private readonly IAsyncRepository<Slot> _slotRepository;
        private readonly ISlotService _slotService;
        
        public SlotController(
            IAsyncRepository<Slot> slotRepository,
            ISlotService slotService,
            IMapper mapper) : base(mapper)
        {
            _slotRepository = slotRepository;
            _slotService = slotService;
        }

        // [HttpGet("{slotId}")]
        // [SwaggerOperation(
        //     Summary = "Get slot by identification",
        //     Description = "Get slot by identification",
        //     OperationId = "slots-GetById",
        //     Tags = new[] {"SlotEndpoints"})]
        // public async Task<ActionResult<GetByIdSlotResponse>> GetById([FromRoute] Guid slotId, CancellationToken cancellationToken)
        // {
        //     //1.find slot
        //     var slot = await _slotRepository.GetByIdAsync(slotId, cancellationToken);
        //     //2.create slotDTO
        //     var slotDto = new SlotDto()
        //     {
        //         
        //     };
        //     //3.create response
        //     //4.assign response.Slot = slotDto
        //     //5.return response
        // }
        
        [HttpPost]
        [SwaggerOperation(
            Summary = "Create new Slot",
            Description = "Create new Slot",
            OperationId = "slots-Create",
            Tags = new[] {"SlotEndpoints"})]
        public async Task<ActionResult<CreateSlotResponse>> Post([FromForm] CreateSlotRequest request,
            CancellationToken cancellationToken)
        {
            var slot = new Slot(
                request.Title,
                request.Description,
                request.CategoryId,
                request.OwnerId,
                null,
                request.StartPrice
            );
            await _slotService.AddPicturesToSlot(slot, request.Pictures, cancellationToken);
            
            slot = await _slotRepository.AddAsync(slot, cancellationToken);
            
            var response = new CreateSlotResponse(request.CorrelationId());
            response.SlotId = slot.Id;

            return response;
        }
    }
}