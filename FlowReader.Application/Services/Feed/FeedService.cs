using AutoMapper;
using FlowReader.Application.Exceptions;
using FlowReader.Application.Models;
using FlowReader.Core.Entities;
using FlowReader.DataAccess.Repositories;
using FlowReader.Shared.Services;

namespace FlowReader.Application.Services
{
    public class FeedService : IFeedService
    {
        private readonly IFeedRepository _feedRepository;
        private readonly IClaimService _claimService;
        private readonly IMapper _mapper;

        public FeedService(IFeedRepository feedRepository, IClaimService claimService, IMapper mapper)
        {
            _feedRepository = feedRepository;
            _claimService = claimService;
            _mapper = mapper;
        }

        public async Task<FeedResponseModel> GetByIdAsync(Guid id)
        {
            var result = await _feedRepository.GetFirstAsync(x => x.Id == id);

            return _mapper.Map<FeedResponseModel>(result);
        }

        public async Task<IEnumerable<FeedResponseModel>> GetAllAsync()
        {
            var currentUserId = _claimService.GetUserId();
            var result = await _feedRepository.GetAllAsync(x => x.UserId == currentUserId);

            return _mapper.Map<IEnumerable<FeedResponseModel>>(result);
        }

        public async Task<FeedResponseModel?> CreateAsync(CreateFeedModel feedModel)
        {
            var feed = await ReadFeedAsync(feedModel.Link);
            if(feed == null)
            {
                return null;
            }

            Feed feedDb = new()
            {
                UserTitle = feedModel.Title,
                Title = feed.Title,
                Description = feed.Description,
                Link = feedModel.Link,
                PublishingDate = feed.LastUpdatedDate ?? DateTime.Now                
            };
            if(!feedModel.IsPublic)
            {
                feedDb.UserId = _claimService.GetUserId();
            }

            var addedFeed = await _feedRepository.AddAsync(feedDb);
            return _mapper.Map<FeedResponseModel>(addedFeed);
        }

        public async Task<FeedResponseModel> UpdateAsync(Guid id, UpdateFeedModel feedModel)
        {
            var feed = await _feedRepository.GetFirstAsync(x => x.Id == id);
            feed.UserTitle = feedModel.Title;

            var result = await _feedRepository.UpdateAsync(feed);
            return _mapper.Map<FeedResponseModel>(result);
        }

        public async Task<BaseResponseModel> DeleteAsync(Guid id)
        {
            var feed = await _feedRepository.GetFirstAsync(x => x.Id == id);
            var result = await _feedRepository.DeleteAsync(feed);

            return new BaseResponseModel
            {
                Id = feed.Id
            };
        }

        public async Task<CodeHollow.FeedReader.Feed> ReadFeedAsync(string url)
        {
            try
            {
                var feed = await CodeHollow.FeedReader.FeedReader.ReadAsync(url);
                return feed;
            }
            catch (Exception)
            {
                throw new BadRequestException("Wrong URL. An error occurred while trying to read the feed from a given URL.");
            }
        }
    }
}
