using System;
using System.Collections.Generic;
using Models.Enums;

namespace Models.DTOs.Shop;

public class UpdateEmployee
{
    public Guid EmployeeId { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public EmployeeStatus Status { get; set; }
    public List<AddShopServiceToEmployee> Services { get; set; }
}