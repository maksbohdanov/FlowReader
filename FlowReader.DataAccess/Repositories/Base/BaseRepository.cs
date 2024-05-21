using FlowReader.Core.Common;
using FlowReader.Core.Exceptions;
using FlowReader.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FlowReader.DataAccess.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DatabaseContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseRepository(DatabaseContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entity = await _dbSet.Where(predicate).FirstOrDefaultAsync();

            if (entity == null) throw new NotFoundException(typeof(TEntity));

            return entity;
        }

        public virtual async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate)
        {
            return predicate == null
            ? await _dbSet.ToListAsync()
            : await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var addedEntity = (await _dbSet.AddAsync(entity)).Entity;
            await SaveChangesAsync();

            return addedEntity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            var removedEntity = _dbSet.Remove(entity).Entity;
            await SaveChangesAsync();

            return removedEntity;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
