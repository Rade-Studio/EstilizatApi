using System;

namespace Models.DbEntities;

public class ShopReview : BaseEntity
{
    public int CustomerId { get; set; }
    public Guid ShopId { get; set; }
    public int Rating { get; set; }
    public string Review { get; set; }
    public string Reply { get; set; }
    public bool Visibility { get; set; }
    
    public Shop Shop { get; set; }
}