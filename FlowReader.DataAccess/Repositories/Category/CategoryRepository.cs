using FlowReader.Core.Entities;
using FlowReader.Core.Exceptions;
using FlowReader.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FlowReader.DataAccess.Repositories
{
    public class CategoryRepository: BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DatabaseContext context) : base(context)
        {            
        }

        public async override Task<Category> GetFirstAsync(Expression<Func<Category, bool>> predicate)
        {
            var entity = await _dbSet
                .Include(x => x.Users)
                .Where(predicate)
                .FirstOrDefaultAsync();

            if (entity == null) throw new NotFoundException(typeof(Category));

            return entity;
        }

        public async Task<List<Category>> GetAllIncludedAsync(Expression<Func<Category, bool>>? predicate)
        {            
            if(predicate != null)
            {
                return await _context.Categories
                   .Include(x => x.Users)
                   .Where(predicate)
                   .ToListAsync();
            }
            else
            {
                return await _context.Categories
                    .Include(x => x.Users)
                    .Include(x => x.Feeds)
                    .ToListAsync();
            }
        }
    }
}
