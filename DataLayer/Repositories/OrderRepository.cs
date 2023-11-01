﻿using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
	public class OrderRepository : GenericRepository<Order>
	{
		public OrderRepository(SswdatabaseContext context) : base(context)
		{
		}

		
	}
}
