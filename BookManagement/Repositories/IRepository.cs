using BookManagement._3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.Repositories
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities);
        IQueryable<TEntity> AsQueryable();
    }
}
