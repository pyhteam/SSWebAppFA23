using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities;

public class AccountDetail
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public Guid AccountId { get; set; } 

    public string? Avatar { get; set; }
    [Required]
    public string Fullname { get; set; } 

    public string? Address { get; set; }

    public string? Phone { get; set; }
    [Required]
    public bool Gender { get; set; }

    public virtual Account? Account { get; set; } 
}
