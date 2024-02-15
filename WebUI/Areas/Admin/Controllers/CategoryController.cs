using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Results;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WebUI.Areas.Admin.Models;
using WebUI.Extensions;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(UserManager<User> userManager, IMapper mapper, ICategoryService categoryService) : base(userManager, mapper)
        {
            _categoryService = categoryService;
        }
        [Authorize(Roles = "SuperAdmin,Category.Read")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetAllAsync();
            return View(result);
        }
        [Authorize(Roles = "SuperAdmin,Category.Create")]
        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_CategoryAddPartial");
        }
        [Authorize(Roles = "SuperAdmin,Category.Create")]
        [HttpPost]
        public async Task<IActionResult> Add(CategoryAddDto categoryAddDto)
        {

            if (ModelState.IsValid)
            {
                var result = await _categoryService.Addv2Async(categoryAddDto, this.CurrentUser.UserName);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    var model = JsonSerializer.Serialize(new CategoryViewModel
                    {
                        Category = result.Data,
                        CategoryAddPartial = await this.RenderViewToStringAsync("_CategoryAddPartial", categoryAddDto),
                        Message = result.Message
                    });
                    return Json(model);
                }
            }
            var error = JsonSerializer.Serialize(new CategoryViewModel
            {
                CategoryAddPartial = await this.RenderViewToStringAsync("_CategoryAddPartial", categoryAddDto),
                Message = "Eklenmedi!"
            });
            return Json(error);
        }
        [Authorize(Roles = "SuperAdmin,Category.Delete")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            IDataResult<Category> result = await _categoryService.DeleteAsync(id, CurrentUser.UserName);

            var jsonData = JsonSerializer.Serialize(result);
            return Json(jsonData);
       
        }
        [Authorize(Roles = "SuperAdmin,Category.Read")]
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var result =await _categoryService.GetAllAsync();
            return Json(JsonSerializer.Serialize(result.Data,new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            }));
        }
        [Authorize(Roles = "SuperAdmin,Category.Update")]
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _categoryService.GetCategoryUpdateDtoAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return PartialView("_CategoryUpdatePartial", result.Data);
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "SuperAdmin,Category.Update")]
        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateDto categoryUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.UpdateAsync(categoryUpdateDto, CurrentUser.UserName);
                var category = _categoryService.GetAsync(categoryUpdateDto.Id).Result.Data.Category;
                if (result.ResultStatus == ResultStatus.Success)
                {
                    var successCategoryUpdateModel = JsonSerializer.Serialize(new CategoryUpdateViewModel
                    {
                        CategoryUpdatePartial = await this.RenderViewToStringAsync("_CategoryUpdatePartial", categoryUpdateDto),
                        Category = category,
                        Message = "Güncelleme işlemi başarılı!"
                    },new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve
                    });

                    return Json(successCategoryUpdateModel);
                }
            }
            var errorCategoryUpdateModel = JsonSerializer.Serialize(new CategoryUpdateViewModel
            {
                CategoryUpdatePartial = await this.RenderViewToStringAsync("_CategoryUpdatePartial", categoryUpdateDto),
                Category= _categoryService.GetAsync(categoryUpdateDto.Id).Result.Data.Category
            }, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });

            return Json(errorCategoryUpdateModel);
        }

        [Authorize(Roles = "SuperAdmin,Category.Read")]
        [HttpGet]
        public async Task<IActionResult> DeletedCategories()
        {
            var result = await _categoryService.GetAllByDeletedAsync();
            return View(result.Data);
        }
        [Authorize(Roles = "SuperAdmin,Category.Read")]
        [HttpGet]
        public async Task<JsonResult> GetAllDeletedCategories()
        {
            var result = await _categoryService.GetAllByDeletedAsync();
            var categories = JsonSerializer.Serialize(result.Data, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(categories);
        }
        [Authorize(Roles = "SuperAdmin,Category.Update")]
        [HttpPost]
        public async Task<JsonResult> UndoDelete(int categoryId)
        {
            var result = await _categoryService.UndoDeleteAsync(categoryId, CurrentUser.UserName);
            var undoDeletedCategory = JsonSerializer.Serialize(result.Data);
            return Json(undoDeletedCategory);
        }
        [Authorize(Roles = "SuperAdmin,Category.Delete")]
        [HttpPost]
        public async Task<JsonResult> HardDelete(int categoryId)
        {
            var result = await _categoryService.HardDeleteAsync(categoryId);
            var deletedCategory = JsonSerializer.Serialize(result);
            return Json(deletedCategory);
        }
    }
}
