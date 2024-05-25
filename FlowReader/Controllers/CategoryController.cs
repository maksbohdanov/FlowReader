using AutoMapper;
using FlowReader.Application.Models;
using FlowReader.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlowReader.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> UserCategories()
        {
            var categories = await _categoryService.GetUserCategoriesAsync();
            return View(categories);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SaveCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.CreateAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            var model = _mapper.Map<SaveCategoryModel>(category);

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, SaveCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.UpdateAsync(id, model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        //TODO delete

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _categoryService.DeleteAsync(id);
                
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> ToggleFavoriteCategory([FromBody] ToggleFavoriteCategoryModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid request");
            }
            
            await _categoryService.ToggleFavoriteCategoryAsync(model, User);
            return Ok(new { success = true });
        }
    }
}
