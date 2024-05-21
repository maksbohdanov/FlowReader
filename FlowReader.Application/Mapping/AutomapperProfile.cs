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

            CreateMap<CategoryResponseModel, SaveCategoryModel>();
        }
    }
}
