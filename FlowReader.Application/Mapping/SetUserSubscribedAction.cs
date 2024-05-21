using AutoMapper;
using FlowReader.Application.Models;
using FlowReader.Core.Entities;
using FlowReader.Shared.Services;

namespace FlowReader.Application.Mapping
{
    public class SetUserSubscribedAction : IMappingAction<Category, UserCategoryResponseModel>
    {
        private readonly IClaimService _claimService;

        public SetUserSubscribedAction(IClaimService claimService)
        {
            _claimService = claimService;
        }

        public void Process(Category source, UserCategoryResponseModel destination, ResolutionContext context)
        {
            var currentUserId = _claimService.GetUserId();
            bool isSubscribed = source.Users.Any(x => x.Id == currentUserId);
            destination.IsSubscribed = isSubscribed;
        }
    }
}
