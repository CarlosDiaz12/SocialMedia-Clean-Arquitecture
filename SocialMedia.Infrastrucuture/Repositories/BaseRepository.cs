using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastrucuture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infrastrucuture.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly SocialMediaContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;
        public BaseRepository(SocialMediaContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> result = _dbSet;

            if (filter != null)
                result = result.Where(filter);

            return await result.ToListAsync();
        }

        public async Task<TEntity> GetById(int Id)
        {
            return await _dbSet.FindAsync(Id);
        }
        public async Task Delete(int Id)
        {
            var entity = await GetById(Id);
            _dbSet.Remove(entity);
        }

        public async Task Insert(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
                _dbContext.Attach(entity);

            _dbSet.Update(entity);
        }
    }
}
