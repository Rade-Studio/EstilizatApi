#nullable enable
using System;

namespace Models.DTOs.Shop;

public class ReplyShopReview
{
    public Guid ReviewId { get; set; }
    public string? Reply { get; set; }
}