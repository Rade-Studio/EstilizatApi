using System;

namespace Models.DTOs.Shop;

public class AddShopReview
{
    public int Rating { get; set; }
    public string Review { get; set; }
}