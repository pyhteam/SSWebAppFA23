using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities;

public class OrderService
{
    [Key]
    public Guid OrderId { get; set; }
    [Key]
    public Guid ServiceId { get; set; } 

    public virtual Order? Order { get; set; } 

    public virtual Service? Service { get; set; } 
}
