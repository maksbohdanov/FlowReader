using AutoMapper;
using FlowReader.Application.Models;
using FlowReader.Core.Entities;

namespace FlowReader.Application.Mapping
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Feed, FeedResponseModel>();

            CreateMap<News, NewsResponseModel>();

            CreateMap<Category, CategoryResponseModel>();

            CreateMap<Category, UserCategoryResponseModel>()
                .AfterMap<SetUserSubscribedAction>();

            CreateMap<Category, FeedCategoryResponseModel>()
                .ForMember(
                    x => x.IsIncluded, 
                    o => o.MapFrom((src, dest, destMember, context) =>
                    {
                        var feedId = (Guid)context.Items["FeedId"];
                        bool included = src.Feeds.Any(f => f.Id == feedId);
                        return included;
                    }));

            CreateMap<CategoryResponseModel, SaveCategoryModel>();
        }
    }
}
