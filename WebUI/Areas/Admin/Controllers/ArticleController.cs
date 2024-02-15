using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Helpers.Image;
using ProgrammersBlog.Shared.Results;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WebUI.Areas.Admin.Models;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticleController : BaseController
    {
        private readonly IToastNotification _toastNotification;
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly IImageHelper _imageHelper;

        public ArticleController(IArticleService articleService, ICategoryService categoryService, UserManager<User> userManager, IMapper mapper, IImageHelper imageHelper, IToastNotification toastNotification) : base(userManager, mapper)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _imageHelper = imageHelper;
            _toastNotification = toastNotification;
        }
        [Authorize(Roles = "SuperAdmin,Article.Read")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _articleService.GetAllAsync();
            if (result.ResultStatus == ResultStatus.Success)
            {
                return View(result.Data);
            }
            return NotFound();            
        }
        [Authorize(Roles = "SuperAdmin,Article.Create")]
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var result = await _categoryService.GetAllByNonDeletedAsync();
            if (result.ResultStatus == ResultStatus.Success)
            {
                return View(new ArticleAddViewModel
                {
                    Categories = result.Data.Categories
                });
            }
            return NotFound();
        }

        [Authorize(Roles = "SuperAdmin,Article.Create")]
        [HttpPost]
        public async Task<IActionResult> Add(ArticleAddViewModel articleAddViewModel)
        {
            if (ModelState.IsValid)
            {
                var articleAddDto = Mapper.Map<ArticleAddDto>(articleAddViewModel);
                var imageResult = await _imageHelper.UploadImageAsync(CurrentUser.UserName, articleAddViewModel.File,"articleImages");
                if (imageResult.ResultStatus == ResultStatus.Success)
                {
                    articleAddDto.Thumbnail = imageResult.Data.FullName;
                    var articleResult = await _articleService.AddAsync(articleAddDto,CurrentUser.UserName);
                    if (articleResult.ResultStatus == ResultStatus.Success)
                    {
                        _toastNotification.AddSuccessToastMessage(articleResult.Message);
                        return RedirectToAction("Index", "Article");
                    }
                    else
                    {
                        ModelState.AddModelError("", articleResult.Message);
                    }
                }
                else
                {
                    ModelState.AddModelError("", imageResult.Message);
                }
            }
            articleAddViewModel.Categories = _categoryService.GetAllByNonDeletedAsync().Result.Data.Categories;
            return View(articleAddViewModel);
        }

        [Authorize(Roles = "SuperAdmin,Article.Update")]
        [HttpGet]
        public async Task<IActionResult> Update(int articleId)
        {
            var articleUpdateDto = await _articleService.GetArticleUpdateDtoAsync(articleId);
            var categoryResult = await _categoryService.GetAllByNonDeletedAsync();
            if (articleUpdateDto.ResultStatus == ResultStatus.Success && categoryResult.ResultStatus == ResultStatus.Success)
            {
                var model = Mapper.Map<ArticleUpdateViewModel>(articleUpdateDto.Data);
                model.Categories = categoryResult.Data.Categories;
                return View(model);
            }
            return NotFound();
        }

        [Authorize(Roles = "SuperAdmin,Article.Update")]
        [HttpPost]
        public async Task<IActionResult> Update(ArticleUpdateViewModel articleUpdateViewModel)
        {
            if (ModelState.IsValid)
            {
                bool isNewThumbnailUploaded = false;
                var oldThumbnail = articleUpdateViewModel.Thumbnail;
                if (articleUpdateViewModel.ThumbnailFile != null)
                {
                    var uploadedImageResult = await _imageHelper.UploadImageAsync(articleUpdateViewModel.Title,
                        articleUpdateViewModel.ThumbnailFile,"articleImages");
                    articleUpdateViewModel.Thumbnail = uploadedImageResult.ResultStatus == ResultStatus.Success
                        ? uploadedImageResult.Data.FullName
                        : "articleImages/defaultThumbnail.jpg";
                    if (oldThumbnail != "articleImages/defaultThumbnail.jpg")
                    {
                        isNewThumbnailUploaded = true;
                    }
                }
                var articleUpdateDto = Mapper.Map<ArticleUpdateDto>(articleUpdateViewModel);
                var result = await _articleService.UpdateAsync(articleUpdateDto, CurrentUser.UserName);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    if (isNewThumbnailUploaded)
                    {
                        _imageHelper.DeleteImage(oldThumbnail);
                    }
                    _toastNotification.AddSuccessToastMessage(result.Message, new ToastrOptions
                    {
                        Title = "Başarılı İşlem!"
                    });
                    return RedirectToAction("Index", "Article");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }
            }

            var categories = await _categoryService.GetAllByNonDeletedAndActiveAsync();
            articleUpdateViewModel.Categories = categories.Data.Categories;
            return View(articleUpdateViewModel);
        }
        [Authorize(Roles = "SuperAdmin,Article.Delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(int articleId)
        {
            var result = await _articleService.DeleteAsync(articleId, CurrentUser.UserName);
            var articleResult = JsonSerializer.Serialize(result);
            return Json(articleResult);
        }
        [Authorize(Roles = "SuperAdmin,Article.Read")]
        [HttpGet]
        public async Task<JsonResult> GetAllArticles()
        {
            var articles = await _articleService.GetAllByNonDeletedAsync();
            var articleResult = JsonSerializer.Serialize(articles, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(articleResult);
        }
        [Authorize(Roles = "SuperAdmin,Article.Delete")]
        [HttpPost]
        public async Task<JsonResult> HardDelete(int articleId)
        {
            var result = await _articleService.HardDeleteAsync(articleId);
            var hardDeletedArticleResult = JsonSerializer.Serialize(result);
            return Json(hardDeletedArticleResult);
        }
        [Authorize(Roles = "SuperAdmin,Article.Update")]
        [HttpPost]
        public async Task<JsonResult> UndoDelete(int articleId)
        {
            var result = await _articleService.UndoDeleteAsync(articleId, CurrentUser.UserName);
            var undoDeleteArticleResult = JsonSerializer.Serialize(result);
            return Json(undoDeleteArticleResult);
        }
        [Authorize(Roles = "SuperAdmin,Article.Read")]
        [HttpGet]
        public async Task<IActionResult> DeletedArticles()
        {
            var result = await _articleService.GetAllByDeletedAsync();
            return View(result.Data);

        }
        [Authorize(Roles = "SuperAdmin,Article.Read")]
        [HttpGet]
        public async Task<JsonResult> GetAllDeletedArticles()
        {
            var result = await _articleService.GetAllByDeletedAsync();
            var articles = JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(articles);
        }
    }
}
