using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities;

public class Account
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public AccountRole Role { get; set; }
    [Required]
    public AccountStatus Status { get; set; }

    public virtual AccountDetail? AccountDetail { get; set; } 

    public virtual ICollection<Image>? Images { get; set; } 

    public virtual ICollection<Order>? Orders { get; set; }    
}

public enum AccountRole
{
	Customer,
	Admin,
	Staff
}

public enum AccountStatus
{
	Active,
	Inactive
}
