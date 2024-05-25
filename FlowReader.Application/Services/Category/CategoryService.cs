using AutoMapper;
using FlowReader.Application.Models;
using FlowReader.Core.Entities;
using FlowReader.Core.Identity;
using FlowReader.DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace FlowReader.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<CategoryResponseModel> GetByIdAsync(Guid id)
        {
            var result = await _categoryRepository.GetFirstAsync(x => x.Id == id);

            return _mapper.Map<CategoryResponseModel>(result);
        }

        public async Task<IEnumerable<CategoryResponseModel>> GetAllAsync()
        {
            var result = await _categoryRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<CategoryResponseModel>>(result);
        }

        public async Task<IEnumerable<UserCategoryResponseModel>> GetUserCategoriesAsync()
        {
            var result = await _categoryRepository.GetAllIncludedAsync();

            return _mapper.Map<IEnumerable<UserCategoryResponseModel>>(result);
        }

        public async Task<CategoryResponseModel?> CreateAsync(SaveCategoryModel categoryModel)
        {
            Category category = new()
            {
                Code = categoryModel.Code,
                Name = categoryModel.Name
            };

            var addedCategory = await _categoryRepository.AddAsync(category);
            return _mapper.Map<CategoryResponseModel>(addedCategory);
        }

        public async Task<CategoryResponseModel> UpdateAsync(Guid id, SaveCategoryModel categoryModel)
        {
            var category = await _categoryRepository.GetFirstAsync(x => x.Id == id);
            category.Code = categoryModel.Code;
            category.Name = categoryModel.Name;

            var result = await _categoryRepository.UpdateAsync(category);
            return _mapper.Map<CategoryResponseModel>(result);
        }

        public async Task<BaseResponseModel> DeleteAsync(Guid id)
        {
            var category = await _categoryRepository.GetFirstAsync(x => x.Id == id);
            var result = await _categoryRepository.DeleteAsync(category);

            return new BaseResponseModel
            {
                Id = category.Id
            };
        }

        public async Task ToggleFavoriteCategoryAsync(ToggleFavoriteCategoryModel categoryModel, ClaimsPrincipal principal)
        {
            var user = await _userManager.GetUserAsync(principal);            

            var category = await _categoryRepository.GetFirstAsync(x => x.Id == categoryModel.CategoryId);          

            if (categoryModel.IsSubscribed)
            {
                if(!category.Users.Contains(user))
                {
                    category.Users.Add(user);
                }
            }
            else
            {
                if (category.Users.Contains(user))
                {
                    category.Users.Remove(user);
                }
            }

            await _categoryRepository.SaveChangesAsync();         
        }

        public async Task<IEnumerable<FeedCategoryResponseModel>> GetCategoriesByFeed(Guid feedId)
        {
            var result = await _categoryRepository.GetAllIncludedAsync();

            return _mapper.Map<IEnumerable<FeedCategoryResponseModel>>(result, opt => opt.Items["FeedId"] = feedId);
        }
    }
}
