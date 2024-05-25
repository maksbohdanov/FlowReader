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

        //TODO
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    //var feed = await _feedService.GetByIdAsync(id);
        //    var model = new DeleteConfirmationModel { ControllerName = "Feed", ItemName = "feed", Id = id };

        //    return PartialView("_DeleteConfirmation", model);
        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(Guid id)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _feedService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        //todo admin categories
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Categories(Guid id)
        {
            var categories = await _categoryService.GetCategoriesByFeed(id);

            return View();
        }

        [HttpPost, ActionName("Categories")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> FeedCategories(Guid id)
        {
            var categories = await _categoryService.GetCategoriesByFeed(id);

            return View();
        }
    }
}
