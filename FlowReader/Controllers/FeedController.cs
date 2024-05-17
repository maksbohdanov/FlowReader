using FlowReader.Application.Models;
using FlowReader.Application.Services;
using FlowReader.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlowReader.Controllers
{
    public class FeedController : Controller
    {
        private readonly IFeedService _feedService;

        public FeedController(IFeedService feedService)
        {
            _feedService = feedService;
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
    }
}
