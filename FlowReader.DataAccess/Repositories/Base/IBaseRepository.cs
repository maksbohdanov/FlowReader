using FlowReader.Core.Common;
using System.Linq.Expressions;

namespace FlowReader.DataAccess.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate);

        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null);

        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<TEntity> DeleteAsync(TEntity entity);

        Task SaveChangesAsync();
    }
}
