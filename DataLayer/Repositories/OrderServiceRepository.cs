﻿using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class OrderServiceRepository :GenericRepository<OrderService>
    {
        public OrderServiceRepository(SswdatabaseContext context): base(context) { }
    }
}
