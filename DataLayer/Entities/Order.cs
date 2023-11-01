using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities;

public class Order
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public Guid AccountId { get; set; }
    [Required]
    public DateTime OrderDate { get; set; }
    [Required]
    public decimal Total { get; set; }
    [Required]
    public OrderStatus Status { get; set; }

    public virtual Account? Account { get; set; } 

    public virtual FeedBack? FeedBack { get; set; }

    public virtual Payment? Payment { get; set; } 

    public virtual ICollection<OrderProduct>? Products { get; set; }

    public virtual ICollection<OrderService>? Services { get; set; }

}

public enum OrderStatus
{
	Pending,
	Processing,
	Delivering,
	Delivered,
	Cancelled
}
