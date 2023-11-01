﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities;

public class OrderProduct
{
    [Key]
    public Guid OrderId { get; set; }
    [Key]
    public Guid ProductId { get; set; }

    public int Quantity {  get; set; }
    public virtual Order? Order { get; set; }

    public virtual Product? Product { get; set; } 
}
