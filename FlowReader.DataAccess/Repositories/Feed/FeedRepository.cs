using FlowReader.Core.Entities;
using FlowReader.Core.Exceptions;
using FlowReader.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FlowReader.DataAccess.Repositories
{
    public class FeedRepository: BaseRepository<Feed>, IFeedRepository
    {
        public FeedRepository(DatabaseContext context) : base(context)
        {            
        }

        public async override Task<Feed> GetFirstAsync(Expression<Func<Feed, bool>> predicate)
        {
            var entity = await _dbSet
                .Include(x => x.Categories)
                .Where(predicate)
                .FirstOrDefaultAsync();

            if (entity == null) throw new NotFoundException(typeof(Feed));

            return entity;
        }
    }
}
