using FlowReader.Application.Models;

namespace FlowReader.Application.Services
{
    public interface IFeedService
    {
        Task<FeedResponseModel> GetByIdAsync(Guid id);
        Task<IEnumerable<FeedResponseModel>> GetAllAsync();
        Task<IEnumerable<FeedResponseModel>> GetAllPublicAsync();
        Task<FeedResponseModel?> CreateAsync(CreateFeedModel feedModel);
        Task<FeedResponseModel> UpdateAsync(Guid id, UpdateFeedModel feedModel);
        Task<BaseResponseModel> DeleteAsync(Guid id);
        Task<CodeHollow.FeedReader.Feed> ReadFeedAsync(string url);
        Task SaveFeedCategoriesAsync(FeedCategoriesModel feedCategoriesModel);
    }
}
