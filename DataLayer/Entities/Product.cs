using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities;

public class Product
{
    [Key]
    public Guid Id { get; set; }
    [Required]     
    public string Name { get; set; }
    [Required]
    public string Brand { get; set; }

    public string? Color { get; set; }
    [Required]
    public decimal Price { get; set; }

    public string? Catagories { get; set; }
    [Required]
    public double Size { get; set; }

    public string? PictureLink { get; set; }
    public ICollection<OrderProduct>? Orders { get; set; }
}
