using FlowReader.Core.Entities;
using FlowReader.DataAccess.Persistence;

namespace FlowReader.DataAccess.Repositories
{
    public class NewsRepository: BaseRepository<News>, INewsRepository
    {
        public NewsRepository(DatabaseContext context) : base(context)
        {            
        }
    }
}
