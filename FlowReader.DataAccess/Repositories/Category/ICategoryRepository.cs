using FlowReader.Core.Entities;
using System.Linq.Expressions;

namespace FlowReader.DataAccess.Repositories
{
    public interface ICategoryRepository: IBaseRepository<Category>
    {
        Task<List<Category>> GetAllIncludedAsync(Expression<Func<Category, bool>>? predicate = null);
    }
}
