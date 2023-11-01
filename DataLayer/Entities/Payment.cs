using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities;

public class Payment
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public Guid OrderId { get; set; }
    [Required]
    public string Method { get; set; } = null!;
    [Required]
    public DateTime PaymentDate { get; set; }
    [Required]
    public PaymentStatus Status { get; set; }

    public virtual Order? Order { get; set; } = null!;

}


public enum PaymentStatus
{
	Pending = 0,
	Paid = 1,
	Failed = 2
}
