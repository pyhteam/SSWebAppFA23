using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
	public class AccountRepository : GenericRepository<Account>
	{
		public AccountRepository(SswdatabaseContext context) : base(context)
		{
		}

	}
}
