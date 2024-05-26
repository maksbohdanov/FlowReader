using AutoMapper;
using CodeHollow.FeedReader.Feeds;
using FlowReader.Application.Models;
using FlowReader.Core.Entities;
using FlowReader.Core.Identity;
using FlowReader.DataAccess.Repositories;
using FlowReader.Shared.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FlowReader.Application.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;
        private readonly IFeedRepository _feedRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFeedService _feedService;
        private readonly IClaimService _claimService;
        private readonly IMapper _mapper;
        private readonly string _currentUserId;

        public NewsService(INewsRepository newsRepository, IFeedRepository feedRepository, UserManager<ApplicationUser> userManager,
            IFeedService feedService, IClaimService claimService, IMapper mapper)
        {
            _newsRepository = newsRepository;
            _feedRepository = feedRepository;
            _userManager = userManager;
            _feedService = feedService;
            _claimService = claimService;
            _mapper = mapper;

            _currentUserId = _claimService.GetUserId();
        }

        public async Task<IEnumerable<NewsResponseModel>> GetAllAsync()
        {
            await ReadFeedsAsync(x => x.UserId == _currentUserId);
            var result = await _newsRepository.GetAllAsync(x => x.Feed.UserId == _currentUserId);

            return _mapper.Map<IEnumerable<NewsResponseModel>>(result);

        }

        public async Task<IEnumerable<NewsResponseModel>> GetFavoritesAsync(List<Guid>? categoryIds)
        {
            var categoriesQuery =  _userManager.Users
                .Where(x => x.Id == _currentUserId)
                .SelectMany(x => x.Categories);

            if (categoryIds != null && categoryIds.Count != 0)
            {
                categoriesQuery = categoriesQuery.Where(x => categoryIds.Contains(x.Id));
            }

            var feeds = await categoriesQuery
                .SelectMany(x => x.Feeds)
                .Select(f => f.Id)
                .ToListAsync();
            
            await ReadFeedsAsync(x => feeds.Contains(x.Id));


            var categories = await categoriesQuery
                .Select(c => c.Id)
                .ToListAsync();

            var result = await _newsRepository.GetAllAsync(x => x.Feed.Categories.Any(c => categories.Contains(c.Id)));
            return _mapper.Map<IEnumerable<NewsResponseModel>>(result);
        }

        public async Task ReadFeedsAsync(Expression<Func<Feed, bool>> predicate)
        {
            var feeds = await _feedRepository.GetAllAsync(predicate);
            foreach (var feedDb in feeds)
            {
                var feed = await _feedService.ReadFeedAsync(feedDb.Link);
                
                foreach(var item in feed.Items)
                {
                    bool newsExists = (await _newsRepository.GetAllAsync(x => x.ItemId == item.Id)).Count != 0;
                    if (newsExists)
                        continue;

                    News news = new()
                    {
                        FeedId = feedDb.Id,
                        ItemId = item.Id,
                        Title = item.Title,
                        Description = item.Description,
                        Link = item.Link,
                        PublishingDate = item.PublishingDate ?? DateTime.Now
                    };
                    if (item.SpecificItem is Rss20FeedItem feedItem)
                    {
                        news.Image = feedItem?.Enclosure?.Url ?? string.Empty;
                    }

                    await _newsRepository.AddAsync(news);
                }
            }
        }
    }
}
