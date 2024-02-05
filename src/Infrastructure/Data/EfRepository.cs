using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
	public class EfRepository<T> : IRepository<T> where T : BaseEntity
	{
		private readonly WatchHubContext _db;
        public EfRepository(WatchHubContext db)
        {
			_db = db;
        }
        public async Task<T> AddAsync(T entity)
		{
			_db.Add(entity);
			await _db.SaveChangesAsync();
			return entity;
		}

		public Task<int> CountAsync(ISpecification<T> specification)
		{
			return _db.Set<T>().WithSpecification(specification).CountAsync();
		}

		public async Task DeleteAsync(T entity)
		{
			_db.Remove(entity);
			await _db.SaveChangesAsync();
		}

		public Task<T> FirstAsync(ISpecification<T> specification)
		{
			return _db.Set<T>().WithSpecification(specification).FirstAsync();
		}

		public Task<T?> FirstOrDefaultAsync(ISpecification<T> specification)
		{
			return _db.Set<T>().WithSpecification(specification).FirstOrDefaultAsync();
		}

		public async Task<List<T>> GetAllAsync()
		{
			return await _db.Set<T>().ToListAsync();
		}

		public async Task<List<T>> GetAllAsync(ISpecification<T> specification)
		{
			return await _db.Set<T>().ToListAsync(specification);
		}

		public async Task<T?> GetByIdAsync(int id)
		{
			return await _db.FindAsync<T>(id);
		}

		public async Task UpdateAsync(T entity)
		{
			_db.Update(entity);
			await _db.SaveChangesAsync();
		}
	}
}
