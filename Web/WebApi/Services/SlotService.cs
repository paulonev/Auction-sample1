using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using WebApi.AppSettings;
using WebApi.Interfaces;

namespace WebApi.Services
{
    public class SlotService : ISlotService
    {
        private readonly IAsyncRepository<Picture> _pictureRepository;
        private readonly Cloudinary _cloudinary;
        private readonly CloudinaryOptions _cloudinaryOptions;
        
        public SlotService(
            IAsyncRepository<Picture> pictureRepository,
            IOptions<CloudinaryOptions> cloudinaryOptions)
        {
            _pictureRepository = pictureRepository;
            _cloudinaryOptions = cloudinaryOptions.Value;
            
            var account = new Account(
                _cloudinaryOptions.CloudName,
                _cloudinaryOptions.ApiKey,
                _cloudinaryOptions.ApiSecret);

            _cloudinary = new Cloudinary(account);
        }
        
        public async Task<Slot> AddPicturesToSlot(Slot slot, IEnumerable<IFormFile> formPictures,
            CancellationToken cancellationToken)
        {
            var pictures = await CreatePictures(slot.Id, formPictures);
            await _pictureRepository.AddRangeAsync(pictures, false, cancellationToken);
            slot.AddPictureRange(pictures);
            return slot;
        }
        
        private async Task<List<Picture>> CreatePictures(Guid slotId, IEnumerable<IFormFile> formPictures)
        {
            var uploadResults = new List<ImageUploadResult>();
            
            foreach (var picture in formPictures)
            {
                string pictureName = $"picture${Guid.NewGuid()}";
                var uploadParams = new ImageUploadParams
                {
                    PublicId = pictureName,
                    File = new FileDescription(pictureName, picture.OpenReadStream()),
                    Folder = $"{slotId}"
                };
                
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                uploadResults.Add(uploadResult);
            }

            var pictures = uploadResults.Select(uploadResult => new Picture
            {
                Id = Guid.Parse(uploadResult.PublicId.Substring(uploadResult.PublicId.LastIndexOf('$') + 1)),
                Name = uploadResult.OriginalFilename,
                ItemId = slotId,
                PictureUri = uploadResult.SecureUrl.AbsoluteUri
            }).ToList();

            return pictures;
        }
    }
}