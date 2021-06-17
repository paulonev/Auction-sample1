using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Http;

namespace WebApi.Interfaces
{
    public interface ISlotService
    {
        Task<Slot> AddPicturesToSlot(Slot slot, IEnumerable<IFormFile> pictures, CancellationToken cancellationToken);
    }
}    