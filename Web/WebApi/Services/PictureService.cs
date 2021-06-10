using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using WebApi.ApiEndpoints.SlotEndpoints;
using WebApi.AppSettings;
using WebApi.Interfaces;

namespace WebApi.Services
{
    public class PictureService : IPictureService
    {
        private readonly IAsyncRepository<Picture> _pictureRepository;
        private readonly Cloudinary _cloudinary;
        private readonly CloudinaryOptions _cloudinaryOptions;
        
        public PictureService(
            IAsyncRepository<Picture> pictureRepository,
            Cloudinary cloudinary,
            CloudinaryOptions cloudinaryOptions)
        {
            _pictureRepository = pictureRepository;
            _cloudinary = cloudinary;
            _cloudinaryOptions = cloudinaryOptions;
        }
        
        public async Task<List<Picture>> CreatePictures(IEnumerable<IFormFile> formPictures, Guid slotId)
        {
            var uploadResults = new List<ImageUploadResult>();
            
            foreach (var picture in formPictures)
            {
                var guid = Guid.NewGuid().ToString();
                var uploadParams = new ImageUploadParams
                {
                    PublicId = Guid.NewGuid().ToString(),
                    File = new FileDescription(guid, picture.OpenReadStream()),
                    Folder = $"{slotId}"
                };
                
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                uploadResults.Add(uploadResult);
            }
            
            var pictures = uploadResults.Select(picture => new Picture
            {
                Id = Guid.Parse(picture.PublicId.Substring(picture.PublicId.LastIndexOf('/') + 1)),
                Name = picture.OriginalFilename,
                ItemId = slotId,
                PictureUri = picture.SecureUrl.AbsoluteUri
            }).ToList();

            return pictures;
        }
    }
}