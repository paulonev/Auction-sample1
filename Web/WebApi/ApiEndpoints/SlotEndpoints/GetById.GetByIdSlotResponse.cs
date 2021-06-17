using System;
using WebApi.Common;

namespace WebApi.ApiEndpoints.SlotEndpoints
{
    public class GetByIdSlotResponse : BaseResponse
    {
        public GetByIdSlotResponse(Guid correlationId) : base(correlationId)
        {    
        }

        public SlotDto Slot { get; set; }
    }
}