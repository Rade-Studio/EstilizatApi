using System;

namespace Models.DTOs.Shop;

public class AddServiceToShop
{
    public Guid ServiceId { get; set; }
    public Guid EmployeeId { get; set; }
}