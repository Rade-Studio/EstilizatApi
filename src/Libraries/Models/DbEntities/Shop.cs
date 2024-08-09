using System.Collections.Generic;
using Models.DbEntities.JsonEntities;

namespace Models.DbEntities;

public class Shop: BaseEntity
{
    public int OwnerId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string PostalCode { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string ProfileImage { get; set; }
    public string ServiceDescription { get; set; }
    public decimal AverageRating { get; set; }
    public int TotalReviews { get; set; }
    public string Website { get; set; }
    public bool IsVerified { get; set; }
    public bool IsActive { get; set; }

    public List<OpeningHour> OpeningHours { get; set; } = [];
    public List<GalleryShop> Gallery { get; set; } = [];
    public List<SocialMedia> SocialMedia { get; set; } = [];
    
    public ICollection<ShopService> ShopServices { get; set; }
    public ICollection<ShopSetting> ShopSettings { get; set; }
}