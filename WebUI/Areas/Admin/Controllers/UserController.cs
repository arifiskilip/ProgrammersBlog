using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Services.Extensions;
using ProgrammersBlog.Shared.Helpers.Image;
using ProgrammersBlog.Shared.Results;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WebUI.Areas.Admin.Models;
using WebUI.Extensions;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        private readonly IImageHelper _imageHelper;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IWebHostEnvironment env, IMapper mapper, IImageHelper imageHelper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _env = env;
            _mapper = mapper;
            _imageHelper = imageHelper;
        }

        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(new UserListDto { Users = users,ResultStatus=ResultStatus.Success,Message="Listeleme işlemi başarılı!"});
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            if (ModelState.IsValid)
            {
                var checkUser = await _userManager.FindByEmailAsync(userLoginDto.Email);
                if (checkUser != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(checkUser, userLoginDto.Password,userLoginDto.RememberMe,false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "E-posta adresiniz veya şifreniz yanlıştır.");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "E-posta adresiniz veya şifreniz yanlıştır.");
                    return View();
                }
            }
            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { Area = "" });
        }

        [Authorize(Roles="Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
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

        [Authorize(Roles ="Admin")]
        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_UserAddPartial");
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(UserAddDto userAddDto)
        {
            if (ModelState.IsValid)
            {
                var imageResult = await _imageHelper.UploadImage(userAddDto.UserName, userAddDto.PictureFile);
                userAddDto.Picture = imageResult.ResultStatus == ResultStatus.Success ? imageResult.Data.FullName : "userImages/default.png";
                var user = _mapper.Map<User>(userAddDto);
                var result = await _userManager.CreateAsync(user, userAddDto.Password);
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
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
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

        [HttpGet]  //Erişim Yok
        public ViewResult AccessDenied()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> Delete(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
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
                foreach (var error in result.Errors)
                {
                    errorMessage += $"*{error.Description}\n";
                }

                var userErrorModel = JsonSerializer.Serialize(new UserDto
                {
                    Message = errorMessage,
                    ResultStatus = ResultStatus.Error,
                    User = user
                });

                return Json(userErrorModel);
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<PartialViewResult> Update(int userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var userUpdateDto = _mapper.Map<UserUpdateDto>(user);
            return PartialView("_UserUpdatePartial", userUpdateDto);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateDto userUpdateDto)
        {
            if (ModelState.IsValid)
            {
                bool isNewPictureUploaded = false; // Yeni resim ekledi mi ?
                var oldUser = await _userManager.FindByIdAsync(userUpdateDto.Id.ToString());
                var oldUserPicture = oldUser.Picture;
                if (userUpdateDto.PictureFile != null)
                {
                    var imageResult = await _imageHelper.UploadImage(userUpdateDto.UserName, userUpdateDto.PictureFile);
                    userUpdateDto.Picture = imageResult.Data.FullName;
                    isNewPictureUploaded = true;
                }

                var updatedUser = _mapper.Map<UserUpdateDto, User>(userUpdateDto, oldUser);
                var result = await _userManager.UpdateAsync(updatedUser);
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
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
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
                    User = _userManager.Users.FirstOrDefaultAsync(x=> x.Id == userUpdateDto.Id).Result,
                    UserUpdatePartial = await this.RenderViewToStringAsync("_UserUpdatePartial", userUpdateDto)
                });
                return Json(userUpdateModelStateErrorViewModel);
            }
        }


        [Authorize]
        [HttpGet]
        public async Task<ViewResult> ChangeDetails()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var updateDto = _mapper.Map<UserUpdateDto>(user);
            return View(updateDto);
        }
        [Authorize]
        [HttpPost]
        public async Task<ViewResult> ChangeDetails(UserUpdateDto userUpdateDto)
        {
            if (ModelState.IsValid)
            {
                bool isNewPictureUploaded = false;
                var oldUser = await _userManager.GetUserAsync(HttpContext.User);
                var oldUserPicture = oldUser.Picture;
                if (userUpdateDto.PictureFile != null)
                {
                    var imageResult = await _imageHelper.UploadImage(userUpdateDto.UserName, userUpdateDto.PictureFile);
                    userUpdateDto.Picture = imageResult.Data.FullName;
                    if (oldUserPicture != "defaultUser.png")
                    {
                        isNewPictureUploaded = true;
                    }

                }

                var updatedUser = _mapper.Map<UserUpdateDto, User>(userUpdateDto, oldUser);
                var result = await _userManager.UpdateAsync(updatedUser);
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
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

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
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var isVerified = await _userManager.CheckPasswordAsync(user, userPasswordChangeDto.CurrentPassword);
                if (isVerified)
                {
                    var result = await _userManager.ChangePasswordAsync(user, userPasswordChangeDto.CurrentPassword,
                        userPasswordChangeDto.NewPassword);
                    if (result.Succeeded)
                    {
                        await _userManager.UpdateSecurityStampAsync(user);
                        await _signInManager.SignOutAsync();
                        await _signInManager.PasswordSignInAsync(user, userPasswordChangeDto.NewPassword, true, false);
                        TempData.Add("SuccessMessage", $"Şifreniz başarıyla değiştirilmiştir.");
                        return View();
                    }
                    else
                    {
                        foreach (var err in result.Errors)
                        {
                            ModelState.AddModelError("", err.Description);
                        }
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
