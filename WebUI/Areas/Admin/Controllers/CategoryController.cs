using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetAll();
            return View(result);
        }

        public IActionResult Add()
        {
            return PartialView("_CategoryAddPartial");
        }
        [HttpPost]
        public async Task<IActionResult> Add(CategoryAddDto categoryAddDto)
        {

            if (ModelState.IsValid)
            {
                var result = await _categoryService.Addv2(categoryAddDto, "Arif");
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
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            IDataResult<Category> result = await _categoryService.Delete(id, "Arif İskilip");

            var jsonData = JsonSerializer.Serialize(result);
            return Json(jsonData);
       
        }

        public async Task<IActionResult> GetAllCategories()
        {
            var result =await _categoryService.GetAll();
            return Json(JsonSerializer.Serialize(result.Data,new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            }));
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _categoryService.GetCategoryDtoAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return PartialView("_CategoryUpdatePartial", result.Data);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateDto categoryUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.Update(categoryUpdateDto, "Arif İskilip");
                if (result.ResultStatus == ResultStatus.Success)
                {
                    var successCategoryUpdateModel = JsonSerializer.Serialize(new CategoryUpdateViewModel
                    {
                        CategoryUpdatePartial = await this.RenderViewToStringAsync("_CategoryUpdatePartial", categoryUpdateDto),
                        Category = _categoryService.Get(categoryUpdateDto.Id).Result.Data.Category,
                        Message = "Güncelleme işlemi başarılı!"
                    });

                    return Json(successCategoryUpdateModel);
                }
            }
            var errorCategoryUpdateModel = JsonSerializer.Serialize(new CategoryUpdateViewModel
            {
                CategoryUpdatePartial = await this.RenderViewToStringAsync("_CategoryUpdatePartial", categoryUpdateDto),
                Category = _categoryService.Get(categoryUpdateDto.Id).Result.Data.Category,
                Message = "Güncelleme işlemi başarısız!"
            });

            return Json(errorCategoryUpdateModel);
        }
    }
}
