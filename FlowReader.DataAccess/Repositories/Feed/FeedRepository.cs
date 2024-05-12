using FlowReader.Core.Entities;
using FlowReader.DataAccess.Persistence;

namespace FlowReader.DataAccess.Repositories
{
    public class FeedRepository: BaseRepository<Feed>, IFeedRepository
    {
        public FeedRepository(DatabaseContext context) : base(context)
        {            
        }
    }
}
