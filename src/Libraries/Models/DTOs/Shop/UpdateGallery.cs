using System.Collections.Generic;

namespace Models.DTOs.Shop;

public class UpdateGallery
{
    public List<GalleryImageItem> Images { get; set; }

    public class GalleryImageItem
    {
        public string Url { get; set; }
        public string Description { get; set; }
    }
}