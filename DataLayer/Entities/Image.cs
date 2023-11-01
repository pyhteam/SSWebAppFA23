using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities;

public class Image
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public Guid AccountId { get; set; }
    [Required]
    public string Drawing { get; set; }
    [Required]
    public string Style { get; set; }
    [Required]
    public string KindofPaint { get; set; } 

    public string? Accessory { get; set; }

    public string? Notes { get; set; }

    public virtual Account? Account { get; set; } 
}
