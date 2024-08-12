using System;
using Models.Enums;

namespace Models.DbEntities;

public class Appointment : BaseEntity
{
    public Guid CustomerId { get; set; }
    public Guid ShopId { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid ServiceId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Status Status { get; set; }
    public string Note { get; set; }
    public string CancelReason { get; set; }
    public bool ReminderSent { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    
    public Shop Shop { get; set; }
    public ShopService ShopService { get; set; }
    public Employee Employee { get; set; }
    
}