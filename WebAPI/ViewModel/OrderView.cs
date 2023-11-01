using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Entities;

namespace WebAPI.ViewModel
{
    public class OrderView
    {
        public Guid OrderId { get; set; }
        public Guid AccountId { get; set; }
        public string? AccountName { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus? Status { get; set; }

        public List<OrderProductView>? OrderProducts { get; set; }

    }

    public class OrderProductView
    {
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
    }
}