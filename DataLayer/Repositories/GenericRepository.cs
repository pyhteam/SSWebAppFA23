using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
	public class GenericRepository<T> where T : class
	{
		private readonly SswdatabaseContext _context;
		private readonly DbSet<T> _dbSet;

		public GenericRepository(SswdatabaseContext context)
		{
			_context = context;
			_dbSet = context.Set<T>();
		}

		public ICollection<T> GetAll()
		{
			return _dbSet.ToList();
		}

		public ICollection<T> GetList(Expression<Func<T,bool>> expression)
		{
			return _dbSet.Where(expression).ToList();
		}

		public T Get(Expression<Func<T, bool>> expression)
		{
			return _dbSet.FirstOrDefault(expression);
		}

		public T Add(T entity)
		{
			_dbSet.Add(entity);
			return entity;
		}
		public void AddRange(ICollection<T> entities)
		{
			_dbSet.AddRange(entities);
		}

		public void Update(T entity)
		{
			_dbSet.Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
			_context.SaveChanges();
		}

	
		public void Delete(Guid id)
		{
			var entity = _dbSet.Find(id);
			_dbSet.Remove(entity);
			_context.SaveChanges();
		}


		public int SaveChanges()
		{
			return _context.SaveChanges();
		}

		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}
	}

}
