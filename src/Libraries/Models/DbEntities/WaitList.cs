using System;
using Models.DbEntities.JsonEntities.WaitList;
using Models.Enums;

namespace Models.DbEntities;

public class WaitList : BaseEntity
{
    public Guid ShopId { get; set; }
    public Guid ServiceId { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime RequestedDate { get; set; }
    public PreferredTimeRange PreferredTimeRange { get; set; }
    public Status Status { get; set; }
    
    public Shop Shop { get; set; }
    public ShopService ShopService { get; set; }
    public Employee Employee { get; set; }
}