using FlowReader.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlowReader.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        public async Task<IActionResult> Index()
        {
            var news = await _newsService.GetAllAsync();
            return View(news);
        }

        public async Task<IActionResult> Recommended()
        {
            var news = await _newsService.GetFavoritesAsync();
            return View(nameof(Index), news);
        }
    }
}
