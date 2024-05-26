using FlowReader.Application.Services;
using FlowReader.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlowReader.Controllers
{
    [Authorize(Roles = "User")]
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly ICategoryService _categoryService;

        public NewsController(INewsService newsService, ICategoryService categoryService)
        {
            _newsService = newsService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var news = await _newsService.GetAllAsync();
            return View(news);
        }

        public async Task<IActionResult> Recommended()
        {
            var news = await _newsService.GetFavoritesAsync();
            var categories = await _categoryService.GetUserCategoriesFilterAsync();

            var model = new NewsViewModel
            {
                News = news,
                Categories = categories
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> FilterByCategories([FromBody] List<Guid> categoryIds)
        {
            var news = await _newsService.GetFavoritesAsync(categoryIds);

            return PartialView("_NewsListPartial", news);
        }
    }
}
