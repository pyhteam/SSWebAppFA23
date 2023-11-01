using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities;

public partial class FeedBack
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public Guid OrderId { get; set; } 

    public string? Comment { get; set; }

    public int? Rate { get; set; }

    public string? Picture { get; set; }

    public virtual Order? Order { get; set; } = null!;
}
