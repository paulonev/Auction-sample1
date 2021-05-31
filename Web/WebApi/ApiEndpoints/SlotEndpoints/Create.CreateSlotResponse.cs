using System;
using WebApi.Common;

namespace WebApi.ApiEndpoints.SlotEndpoints
{
    public class CreateSlotResponse : BaseResponse
    {
        public CreateSlotResponse(Guid correlationId) : base(correlationId)
        {
        }

        public Guid SlotId { get; set; }
    }
}