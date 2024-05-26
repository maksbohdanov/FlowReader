using FlowReader.Application.Models;
using FlowReader.Core.Entities;
using System.Linq.Expressions;

namespace FlowReader.Application.Services
{
    public interface INewsService
    {
        Task ReadFeedsAsync(Expression<Func<Feed, bool>> predicate);
        Task<IEnumerable<NewsResponseModel>> GetAllAsync();
        Task<IEnumerable<NewsResponseModel>> GetFavoritesAsync(List<Guid>? categoryIds = null);
    }
}
