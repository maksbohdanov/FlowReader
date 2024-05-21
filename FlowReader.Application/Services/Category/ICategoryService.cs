using FlowReader.Application.Models;
using System.Security.Claims;

namespace FlowReader.Application.Services
{
    public interface ICategoryService
    {
        Task<CategoryResponseModel> GetByIdAsync(Guid id);
        Task<IEnumerable<CategoryResponseModel>> GetAllAsync();
        Task<IEnumerable<UserCategoryResponseModel>> GetUserCategoriesAsync();
        Task<CategoryResponseModel?> CreateAsync(SaveCategoryModel categoryModel);
        Task<CategoryResponseModel> UpdateAsync(Guid id, SaveCategoryModel categoryModel);
        Task<BaseResponseModel> DeleteAsync(Guid id);
        Task ToggleFavoriteCategoryAsync(ToggleFavoriteCategoryModel categoryModel, ClaimsPrincipal principal);
    }
}

