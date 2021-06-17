using System;

namespace ApplicationCore.Entities
{
    public class Picture
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        /// <summary>
        /// represents: file location on the cloudinary server filesystem
        /// </summary>
        public string PictureUri { get; set; }

        public Guid ItemId { get; set; }
        public Slot Item { get; set; }

        public Picture()
        {
        }
        
        public Picture(
            string name,
            string pictureUri,
            Guid itemId)
        {
            Id = Guid.NewGuid();
            Name = name;
            PictureUri = pictureUri;
            ItemId = itemId;
        }

        public void UpdateDetails(string newPictureUri, Guid newItemId)
        {
            ItemId = newItemId;
            //update pictureUri
        }
    }
}