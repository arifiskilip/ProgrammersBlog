using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Shared.Helpers.Image;
using ProgrammersBlog.Shared.Results;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WebUI.Areas.Admin.Models;
using WebUI.Extensions;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : BaseController
    {
        private readonly IImageHelper _imageHelper;
        private readonly SignInManager<User> _signInManager;

        public UserController(UserManager<User> userManager, IMapper mapper, IImageHelper imageHelper, SignInManager<User> signInManager) : base(userManager, mapper)
        {
            _imageHelper = imageHelper;
            _signInManager = signInManager;
        }

        [Authorize(Roles = "SuperAdmin,User.Read")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await UserManager.Users.ToListAsync();
            return View(new UserListDto { Users = users,ResultStatus=ResultStatus.Success,Message="Listeleme işlemi başarılı!"});
        }

        [Authorize(Roles = "SuperAdmin,User.Read")]
        [HttpGet]
        public async Task<PartialViewResult> GetDetail(int userId)
        {
            var user = await UserManager.Users.SingleOrDefaultAsync(u => u.Id == userId);
            return PartialView("_GetDetailPartial", new UserDto { User = user });
        }

        [Authorize(Roles = "SuperAdmin,User.Read")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {   
            var users = await UserManager.Users.ToListAsync();
            var userListDto = JsonSerializer.Serialize(new UserListDto
            {
                Users = users,
                ResultStatus = ResultStatus.Success,
                Message = "Listeleme işlemi başarılı!"
            }, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });

            return Json(userListDto);
        }

        [Authorize(Roles = "SuperAdmin,User.Create")]
        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_UserAddPartial");
        }
        [Authorize(Roles = "SuperAdmin,User.Create")]
        [HttpPost]
        public async Task<IActionResult> Add(UserAddDto userAddDto)
        {
            if (ModelState.IsValid)
            {
                var imageResult = await _imageHelper.UploadImageAsync(userAddDto.UserName, userAddDto.PictureFile);
                userAddDto.Picture = imageResult.ResultStatus == ResultStatus.Success ? imageResult.Data.FullName : "userImages/default.png";
                var user = Mapper.Map<User>(userAddDto);
                var result = await UserManager.CreateAsync(user, userAddDto.Password);
                if (result.Succeeded)
                {
                    var userAddAjaxModel = JsonSerializer.Serialize(new UserAddAjaxModel
                    {
                        User = user,
                        UserAddPartial = await this.RenderViewToStringAsync("_UserAddPartial"),
                        Message = "Ekleme işlemi başarılı!"
                    });
                    return Json(userAddAjaxModel);
                }
                else
                {
                    this.AddModelErrors(result);
                    var userAddErrorAjaxModel = JsonSerializer.Serialize(new UserAddAjaxModel
                    {
                        UserAddPartial = await this.RenderViewToStringAsync("_UserAddPartial",userAddDto),
                        Message = "Ekleme işlemi başarısız!"
                    });

                    return Json(userAddErrorAjaxModel);
                }
            }
            var userAddNotValidError = JsonSerializer.Serialize(new UserAddAjaxModel
            {
                UserAddPartial = await this.RenderViewToStringAsync("_UserAddPartial", userAddDto),
                Message = "Ekleme işlemi başarısız!"
            }) ;

            return Json(userAddNotValidError);
        }

        [Authorize(Roles = "SuperAdmin,User.Delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(int userId)
        {
            var user = await UserManager.FindByIdAsync(userId.ToString());
            var result = await UserManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                if (user.Picture != "userImages/defaultUser.png")
                    _imageHelper.DeleteImage(user.Picture);
                var deletedUser = JsonSerializer.Serialize(new UserDto
                {
                    User = user,
                    ResultStatus = ResultStatus.Success,
                    Message = "Kullanıcı başarıyla silindi!"
                });

                return Json(deletedUser);
            }
            else
            {
                string errorMessage = string.Empty;
                this.AddModelErrors(result);

                var userErrorModel = JsonSerializer.Serialize(new UserDto
                {
                    Message = errorMessage,
                    ResultStatus = ResultStatus.Error,
                    User = user
                });

                return Json(userErrorModel);
            }
        }


        [Authorize(Roles = "SuperAdmin,User.Update")]
        [HttpGet]
        public async Task<PartialViewResult> Update(int userId)
        {
            var user = await UserManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var userUpdateDto = Mapper.Map<UserUpdateDto>(user);
            return PartialView("_UserUpdatePartial", userUpdateDto);
        }
        [Authorize(Roles = "SuperAdmin,User.Update")]
        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateDto userUpdateDto)
        {
            if (ModelState.IsValid)
            {
                bool isNewPictureUploaded = false; // Yeni resim ekledi mi ?
                var oldUser = await UserManager.FindByIdAsync(userUpdateDto.Id.ToString());
                var oldUserPicture = oldUser.Picture;
                if (userUpdateDto.PictureFile != null)
                {
                    var imageResult = await _imageHelper.UploadImageAsync(userUpdateDto.UserName, userUpdateDto.PictureFile);
                    userUpdateDto.Picture = imageResult.Data.FullName;
                    isNewPictureUploaded = true;
                }

                var updatedUser = Mapper.Map<UserUpdateDto, User>(userUpdateDto, oldUser);
                var result = await UserManager.UpdateAsync(updatedUser);
                if (result.Succeeded)
                {
                    if (isNewPictureUploaded)
                    {
                        _imageHelper.DeleteImage(oldUserPicture); //Eski resmi sil
                    }

                    var userUpdateViewModel = JsonSerializer.Serialize(new UserUpdateAjaxModel
                    {
                        ResultStatus = ResultStatus.Success,
                        Message = $"{updatedUser.UserName} adlı kullanıcı başarıyla güncellenmiştir.",
                        User = updatedUser,
                        UserUpdatePartial = await this.RenderViewToStringAsync("_UserUpdatePartial", userUpdateDto)
                    });
                    return Json(userUpdateViewModel);
                }
                else
                {
                    this.AddModelErrors(result);
                    var userUpdateErorViewModel = JsonSerializer.Serialize(new UserUpdateAjaxModel
                    {
                        ResultStatus = ResultStatus.Error,
                        Message = $"{updatedUser.UserName} adlı kullanıcı güncellenemedi!",
                        User = updatedUser,
                        UserUpdatePartial = await this.RenderViewToStringAsync("_UserUpdatePartial", userUpdateDto)
                    });
                    return Json(userUpdateErorViewModel);
                }

            }
            else
            {
                var userUpdateModelStateErrorViewModel = JsonSerializer.Serialize(new UserUpdateAjaxModel
                {
                    Message = $"İşlem Sırasında hata meydana geldi!",
                    User = UserManager.Users.FirstOrDefaultAsync(x=> x.Id == userUpdateDto.Id).Result,
                    UserUpdatePartial = await this.RenderViewToStringAsync("_UserUpdatePartial", userUpdateDto)
                });
                return Json(userUpdateModelStateErrorViewModel);
            }
        }


        [Authorize]
        [HttpGet]
        public async Task<ViewResult> ChangeDetails()
        {
            var user = await UserManager.GetUserAsync(HttpContext.User);
            var updateDto = Mapper.Map<UserUpdateDto>(user);
            return View(updateDto);
        }
        [Authorize]
        [HttpPost]
        public async Task<ViewResult> ChangeDetails(UserUpdateDto userUpdateDto)
        {
            if (ModelState.IsValid)
            {
                bool isNewPictureUploaded = false;
                var oldUser = await UserManager.GetUserAsync(HttpContext.User);
                var oldUserPicture = oldUser.Picture;
                if (userUpdateDto.PictureFile != null)
                {
                    var imageResult = await _imageHelper.UploadImageAsync(userUpdateDto.UserName, userUpdateDto.PictureFile);
                    userUpdateDto.Picture = imageResult.Data.FullName;
                    if (oldUserPicture != "defaultUser.png")
                    {
                        isNewPictureUploaded = true;
                    }

                }

                var updatedUser = Mapper.Map<UserUpdateDto, User>(userUpdateDto, oldUser);
                var result = await UserManager.UpdateAsync(updatedUser);
                if (result.Succeeded)
                {
                    if (isNewPictureUploaded)
                    {
                        _imageHelper.DeleteImage(oldUserPicture);
                    }
                    TempData.Add("SuccessMessage", $"{updatedUser.UserName} adlı kullanıcı başarıyla güncellenmiştir.");
                    return View(userUpdateDto);
                }
                else
                {
                    this.AddModelErrors(result);

                    return View(userUpdateDto);
                }

            }
            else
            {
                return View(userUpdateDto);
            }
        }
        [Authorize]
        [HttpGet]
        public ViewResult PasswordChange()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PasswordChange(UserPasswordChangeDto userPasswordChangeDto)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.GetUserAsync(HttpContext.User);
                var isVerified = await UserManager.CheckPasswordAsync(user, userPasswordChangeDto.CurrentPassword);
                if (isVerified)
                {
                    var result = await UserManager.ChangePasswordAsync(user, userPasswordChangeDto.CurrentPassword,
                        userPasswordChangeDto.NewPassword);
                    if (result.Succeeded)
                    {
                        await UserManager.UpdateSecurityStampAsync(user);
                        await _signInManager.SignOutAsync();
                        await _signInManager.PasswordSignInAsync(user, userPasswordChangeDto.NewPassword, true, false);
                        TempData.Add("SuccessMessage", $"Şifreniz başarıyla değiştirilmiştir.");
                        return View();
                    }
                    else
                    {
                        this.AddModelErrors(result);
                        return View(userPasswordChangeDto);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Lütfen, girmiş olduğunuz şu anki şifrenizi kontrol ediniz.");
                    return View(userPasswordChangeDto);
                }
            }
            else
            {
                return View(userPasswordChangeDto);
            }

            return View();
        }
    }
}
