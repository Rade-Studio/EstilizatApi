using System;

namespace Models.DbEntities;

public class ShopSetting : BaseEntity
{
    public string Key { get; set; } 
    public string Value { get; set; }
    public Guid ShopId { get; set; }
    
    public Shop Shop { get; set; }
}