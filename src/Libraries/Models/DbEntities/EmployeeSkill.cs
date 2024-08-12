using System;

namespace Models.DbEntities;

public class EmployeeSkill
{
    public Guid EmployeeId { get; set; }
    public Guid ShopServiceId { get; set; }
    
    public Employee Employee { get; set; }
    public ShopService Service { get; set; }
}