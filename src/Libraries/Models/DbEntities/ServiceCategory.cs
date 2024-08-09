using System.Collections.Generic;

namespace Models.DbEntities;

public class ServiceCategory : BaseEntity
{
   public string Name { get; set; }
   public string Description { get; set; }
   public int UsedCount { get; set; }
   
   public ICollection<ShopService> ShopServices { get; set; }
}