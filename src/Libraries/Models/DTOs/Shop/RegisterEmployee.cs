using System;
using System.Collections.Generic;

namespace Models.DTOs.Shop;

public class RegisterEmployee
{
    public string Phone { get; set; }
    public string Name { get; set; }
    public List<Guid> Services { get; set; }
}