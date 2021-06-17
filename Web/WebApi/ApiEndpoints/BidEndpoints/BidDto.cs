using System;

namespace WebApi.ApiEndpoints.BidEndpoints
{
    public class BidDto
    {
        public Guid Id { get; set; }
        public string TraderName { get; set; }
        public decimal Amount { get; set; }
    }
}