using System;

namespace ApplicationCore.Entities
{
    public class Picture
    {
        public Guid Id { get; set; }
        
        /// <summary>
        /// Picture url
        /// </summary>
        public string Url { get; set; }

        public Guid ItemId { get; set; }
        public Slot Item { get; set; }
    }
}