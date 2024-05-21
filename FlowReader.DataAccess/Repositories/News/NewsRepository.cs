using FlowReader.Core.Entities;
using FlowReader.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FlowReader.DataAccess.Repositories
{
    public class NewsRepository: BaseRepository<News>, INewsRepository
    {
        public NewsRepository(DatabaseContext context) : base(context)
        {            
        }

        public override async Task<List<News>> GetAllAsync(Expression<Func<News, bool>>? predicate)
        {
            return predicate == null
                ? await base.GetAllAsync(predicate)
                : await _context.News.Include(x => x.Feed).ThenInclude(f => f.Categories)
                    .Where(predicate)
                    .OrderByDescending(x => x.PublishingDate)
                    .ToListAsync();
        }
    }
}
