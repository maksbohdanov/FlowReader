using FlowReader.Application.Models;
using FlowReader.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlowReader.Controllers
{
    [Authorize]
    public class FeedController : Controller
    {
        private readonly IFeedService _feedService;
        private readonly ICategoryService _categoryService;

        public FeedController(IFeedService feedService, ICategoryService categoryService)
        {
            _feedService = feedService;
            _categoryService = categoryService;
        }

        public async Task<ActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction(nameof(Public));
            }

            var feeds = await _feedService.GetAllAsync();
            return View(feeds);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateFeedModel model)
        {
            if (ModelState.IsValid)
            {
                model.IsPublic = User.IsInRole("Admin");
                await _feedService.CreateAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var feed = await _feedService.GetByIdAsync(id);            

            return View(feed);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, FeedResponseModel model)
        {
            if (ModelState.IsValid)
            {
                await _feedService.UpdateAsync(id, new UpdateFeedModel() { Title = model.UserTitle });
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _feedService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Public()
        {
            var feeds = await _feedService.GetAllPublicAsync();

            return View("Index", feeds);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Categories(Guid id)
        {
            var categories = await _categoryService.GetCategoriesByFeed(id);

            return View("~/Views/Feed/Categories.cshtml", categories);
        }

        [HttpPost, ActionName("Categories")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SaveFeedCategories([FromBody] FeedCategoriesModel model)
        {
            await _feedService.SaveFeedCategoriesAsync(model);

            return Ok(new { success = true });
        }
    }
}
