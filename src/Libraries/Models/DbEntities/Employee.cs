using System;
using System.Collections.Generic;
using Models.Enums;

namespace Models.DbEntities;

public class Employee : BaseEntity
{
    public Guid ShopId { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public EmployeeStatus Status { get; set; }
    
    public Shop Shop { get; set; }
    public ICollection<Appointment> Appointments { get; set; }
    public ICollection<WaitList> WaitLists { get; set; }
    public ICollection<EmployeeSkill> Services { get; set; }
    
}