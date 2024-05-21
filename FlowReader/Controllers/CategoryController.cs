using AutoMapper;
using FlowReader.Application.Models;
using FlowReader.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlowReader.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }

        public async Task<IActionResult> UserCategories()
        {
            var categories = await _categoryService.GetUserCategoriesAsync();
            return View(categories);
        }

        public ActionResult Create()
        {
            return View();
        }

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

        public async Task<IActionResult> Edit(Guid id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            var model = _mapper.Map<SaveCategoryModel>(category);

            return View(model);
        }

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

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _categoryService.DeleteAsync(id);
                
            return RedirectToAction(nameof(Index));
        }

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
