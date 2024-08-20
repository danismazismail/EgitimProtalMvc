using ApplicationCore.Entities.UserEntites.Concrete;
using AutoMapper;
using Business.Managers.Interface;
using DTO.Concrete.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WEB.ActionFilters;
using WEB.Models.ViewModels.Users;

namespace WEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(IUserManager userManager, IMapper mapper, IEmailSender emailSender, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _emailSender = emailSender;
            _signInManager = signInManager;
        }

        public IActionResult Login() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<LoginDTO>(model);
                var result = await _userManager.Login(dto);
                if (result)
                {
                    TempData["Success"] = $"Hoşgeldiniz {model.UserName}";
                    
                    if (await _userManager.IsUserInRole(dto.UserName, "admin"))
                        return RedirectToAction("Index", "Dashboard", new { area = "Admin" });

                    if (await _userManager.IsUserInRole(dto.UserName, "customerManager") || await _userManager.IsUserInRole(dto.UserName, "student") || await _userManager.IsUserInRole(dto.UserName, "teacher"))
                        return RedirectToAction("Index", "Home", new { area = "Education" });

                    return RedirectToAction("Index", "Home", new { area = "" });
                }
                TempData["Error"] = "Kullanıcı adı veya şifre yanlış!";
                return View(model);
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        [HttpGet("return-google")]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = "/Account/signin-google" });
            var properties = _userManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet("signin-google")]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            var info = await _userManager.GetExternalLoginInfo();
            if (info == null)
            {
                TempData["Error"] = "Kullanıcı bilgileri yüklenemedi!";
                return RedirectToAction(nameof(Login));
            }

            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            if (email != null)
            {
                var user = await _userManager.FindUser<GetUserDTO>(email);
                if (user == null)
                {
                    TempData["Error"] = "Bu email'e ait kayıtlı bir kullanıcı yoktur!";
                    return RedirectToAction("Login");
                }

                await _userManager.Login(user.Email);
                TempData["Success"] = $"Hoşgeldiniz!";
                return RedirectToAction("Index", "Home");
            }
            TempData["Error"] = "Giriş yaparken bir hata ile karşılaşıldı!";
            return RedirectToAction(nameof(Login));
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                await _userManager.Logout();
                TempData["Success"] = "Çıkış yapıldı!";
                return RedirectToAction("Login");
            }
            TempData["Error"] = "Önce giriş yapınız!";
            return RedirectToAction("Login");
        }

        public IActionResult ForgotPassword() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindUser<GetUserDTO>(model.Email);
                if (user == null)
                {
                    TempData["Error"] = "Bu maile ait bir kayıt bulunamadı. Site yöneticinizle iletişime geçiniz!";
                    return RedirectToAction("Login");
                }

                var token = await _userManager.GenerateTokenForResetPassword(user.Id);

                //URL Çağır
                var callBackURL = Url.Action("ResetPassword", "Account", new { token, email = user.Email }, Request.Scheme);

                //var url = $"{scheme}://{host}/home/index"; // Tam URL oluşturur

                await _emailSender.SendEmailAsync(user.Email, "Şifre Sıfırlama", $"<p>Lütfen şifrenizi sıfırlamak için <a href='{callBackURL}'>buraya tıklayınız!</a></p>");

                return RedirectToAction("ForgotPasswordConfimation");
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        public IActionResult ForgotPasswordConfimation() => View();

        [ValidateTokenExpiryFilter]
        public async Task<IActionResult> ResetPassword(string email, string token = null)
        {
            if (token == null)
            {
                TempData["Error"] = "Token zorunludur!";
                return BadRequest();
            }

            var model = new ResetPasswordVM { Token = token, Email = email };
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindUser<GetUserDTO>(model.Email);
                if (user != null)
                {
                    var dto = _mapper.Map<ResetPasswordDTO>(model);
                    var result = await _userManager.ResetPassword(dto);
                    if (result)
                    {
                        TempData["Success"] = "Şifreniz sıfırlandı. Giriş yapabilirsiniz!";
                        return RedirectToAction("Login");
                    }
                    TempData["Error"] = "Şifreniz değiştirilemedi!";
                    return View(model);
                }
                TempData["Error"] = "Kullanıcı bulunamadı!";
                return RedirectToAction("Login");
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

       
        public async Task<IActionResult> CreatePassword(string email, string token = null)
        {
            if (token == null)
            {
                TempData["Error"] = "Token zorunludur!";
                return BadRequest();
            }

            var model = new CreatePasswordVM { Token = token, Email = email };
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePassword(CreatePasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindUser<GetUserDTO>(model.Email);
                if (user != null)
                {
                    var dto = _mapper.Map<CreatePasswordDTO>(model);
                    var result = await _userManager.ChangePassword(dto);
                    if (result)
                    {
                        TempData["Success"] = "Şifreniz değiştirilidi. Giriş yapabilirsiniz!";
                        return RedirectToAction("Login");
                    }
                    TempData["Error"] = "Şifreniz değiştirilemedi!";
                    return View(model);
                }
                TempData["Error"] = "Kullanıcı bulunamadı!";
                return RedirectToAction("Login");
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> EditUser()
        {
            var dto = await _userManager.FindUser(HttpContext.User);

            if (dto != null)
            {
                var model = _mapper.Map<EditUserVM>(dto);
                return View(model);
            }

            TempData["Error"] = "Bu sayfayı görüntülemek için giriş yapmanız gerekiyor!";
            return RedirectToAction("Login");
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> EditUser(EditUserVM model)
        {
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<EditUserDTO>(model);
                if (dto != null)
                {
                    var result = await _userManager.UpdateUser(dto);
                    if (result)
                    {
                        TempData["Success"] = "Kullanıcı bilgileriniz güncellendi!";
                        return View(model);
                    }
                    TempData["Error"] = "Kullanıcı bilgileri düzenlenemedi!";
                    return View(model);
                }
                TempData["Error"] = "Kullanıcı bulunamadı!";
                return RedirectToAction("Login");
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> ChangePassword()
        {
            var model = new ChangePasswordVM()
            {
                Id = await _userManager.GetUserId(HttpContext.User)
            };
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<ChangePasswordDTO>(model);
                var result = await _userManager.ChangePassword(dto);
                if (result)
                {
                    TempData["Success"] = "Şifreniz başarılı bir şekilde değiştirildi!";
                    return RedirectToAction(nameof(EditUser));
                }
                TempData["Error"] = "Şifreniz değiştirilemedi!";
                return View(model);
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }
    }
}
