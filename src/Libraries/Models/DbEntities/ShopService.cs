using System;
using System.Collections.Generic;
using Models.DbEntities.JsonEntities.ShopService;

namespace Models.DbEntities;

public class ShopService : BaseEntity
{
    public string Name { get; set; } 
    public string Description { get; set; }
    public int Duration { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public bool IsActive { get; set; }
    public Guid ServiceCategoryId { get; set; }
    public Guid ShopId { get; set; }
    public List<MaterialNeeded> Materials { get; set; } = [];
    public List<IncludeProduct> IncludeProducts { get; set; } = [];
    
    public ServiceCategory ServiceCategory { get; set; }
    public Shop Shop { get; set; }
}