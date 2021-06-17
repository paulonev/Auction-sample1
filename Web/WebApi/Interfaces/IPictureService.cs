using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Http;
using WebApi.ApiEndpoints.SlotEndpoints;

namespace WebApi.Interfaces
{
    public interface IPictureService
    {
        Task<List<Picture>> CreatePictures(IEnumerable<IFormFile> formPictures, Guid slotId);

    }
}