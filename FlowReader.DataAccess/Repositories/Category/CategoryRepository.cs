using FlowReader.Core.Entities;
using FlowReader.DataAccess.Persistence;

namespace FlowReader.DataAccess.Repositories
{
    public class CategoryRepository: BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DatabaseContext context) : base(context)
        {            
        }
    }
}
