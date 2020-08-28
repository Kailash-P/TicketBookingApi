using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TicketBooking.DAL.Repositories.Interface;

namespace TicketBooking.DAL.Repositories.Implementation
{
    public class ApplicationRepository<TEntity> : IApplicationRepository<TEntity> where TEntity: class, IEntity
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public ApplicationRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async Task<TEntity> Insert(TEntity entity)
        {
            _dbContext.Add(entity);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            _dbContext.Entry(_dbSet.Find(entity?.Id)).State = EntityState.Detached;
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        public bool Delete(TEntity entity)
        {
            _dbSet.Remove(_dbSet.Find(entity?.Id));
            _dbContext.SaveChanges();
            return true;
        }

        public async Task<TEntity> GetById(object Id)
        {
            var entity = await _dbSet.FindAsync(Id).ConfigureAwait(false);
            return entity;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbSet.AsNoTracking().ToListAsync().ConfigureAwait(false);
        }

        public async Task<TEntity> GetFirstOrDefault(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;

            foreach (Expression<Func<TEntity, object>> include in includes)
                query = query.Include(include);

            return await query.AsNoTracking().FirstOrDefaultAsync(filter).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> GetAllInclude(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = _dbSet;
            if (include != null)
            {
                query = include(query);
            }
            if (orderBy != null)
            {
                return await orderBy(query).AsNoTracking().ToListAsync().ConfigureAwait(false);
            }
            return await query.AsNoTracking().ToListAsync().ConfigureAwait(false);
        }

        public async Task<TEntity> GetFirstOrDefault(Expression<Func<TEntity, bool>> filter = null,
                                          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                          bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (orderBy != null)
            {
                return await orderBy(query).FirstOrDefaultAsync(filter).ConfigureAwait(false);
            }

            return await query.FirstOrDefaultAsync(filter).ConfigureAwait(false);


        }
    }
}
