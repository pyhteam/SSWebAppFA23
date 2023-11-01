using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities;

public class Service
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public string Type { get; set; } = null!;

    public virtual ICollection<OrderService>? Orders { get; set; }
}
