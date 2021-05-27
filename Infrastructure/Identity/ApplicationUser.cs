using System;
using System.ComponentModel.DataAnnotations;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public Guid TraderId { get; set; }
        public Trader Trader { get; set; }
    }
}