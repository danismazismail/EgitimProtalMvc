using ApplicationCore.Consts;
using ApplicationCore.Entities.Concrete;
using AutoMapper;
using Business.Managers.Interface;
using DTO.Concrete.CustomerManagers;
using DTO.Concrete.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using WEB.Areas.Admin.Models.CustomerManagers;


namespace WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class CustomerManagersController : Controller
    {
        private readonly ICMManager _cmService;
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;
        private readonly IEmailSender _emailSender;

        public CustomerManagersController(ICMManager cmService, IMapper mapper, IUserManager userManager, IEmailSender emailSender)
        {
            _cmService = cmService;
            _mapper = mapper;
            _userManager = userManager;
            _emailSender = emailSender;
        }
        public async Task<IActionResult> Index()
        {
            var cms = await _cmService.GetFilteredListAsync
                (
                    select: x => new GetCMVM
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Email = x.Email,
                        BirthDate = x.BirthDate,
                        HireDate = x.HireDate,
                        CreatedDate = x.CreatedDate,
                        UpdatedDate = x.UpdatedDate,
                        Status = x.Status
                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderByDescending(z => z.CreatedDate)
                );


            return View(cms);
        }

        public IActionResult CreateCM() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCM(CreateCMVM model)
        {
            if (ModelState.IsValid)
            {
                var appUserDto = _mapper.Map<CreateUserDTO>(model);
                var resultApp = await _userManager.CreateUser(appUserDto);
                if (resultApp)
                {
                    var appUser = await _userManager.FindUser<GetAppUserDTO>(appUserDto.Email);
                    if (appUser == null)
                    {
                        TempData["Error"] = "Müşteri yöneticisi bulunamadı!";
                        return View(model);
                    }
                    var resultRole = await _userManager.AddUserToRole(appUserDto.Email, "customerManager");
                    if (resultRole)
                    {
                        var dto = _mapper.Map<CreateCMDTO>(model);
                        if (dto is not null)
                        {
                            dto.AppUserId = appUser.Id;
                            var result = await _cmService.AddAsync(dto);
                            if (result)
                            {
                                var token = await _userManager.GenerateTokenForResetPassword(appUser.Id);

                                var callBackURL = Url.Action("CreatePassword", "Account", new { area = "", token, email = appUser.Email }, Request.Scheme);

                                await _emailSender.SendEmailAsync(appUser.Email, "Yeni Kayıt Oluşturma İçin Şifre Değişikliği",
                                    $"<p>Kullanıcı Adınız: {appUser.UserName}</p><br /><p>Şifreniz: 1234</p><br /><p>Lütfen şifrenizi sıfırlamak için <a href='{callBackURL}'>buraya tıklayınız!</a></p>");

                                TempData["Success"] = $"Müşteri yöneticisi sisteme kaydedilmiştir! Giriş bilgileri {appUser.Email} adresine gönderilmiştir!";
                                return RedirectToAction("Index");
                            }
                            TempData["Error"] = "Müşteri Yönetici sisteme eklenemedi!";
                            return View(model);
                        }
                        TempData["Error"] = "Müşteri yöneticisi oluşturulamadı!";
                        return View(model);
                    }
                }

               
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        public async Task<IActionResult> UpdateCM(string id)
        {
            Guid entityID;
            var guidResult = Guid.TryParse(id, out entityID);
            if (!guidResult)
            {
                TempData["Error"] = "Müşteri Yöneticisi bulunamamıştır!";
                return RedirectToAction("Index");
            }

            var cmDTO = await _cmService.GetByIdAsync<UpdateCMDTO>(entityID);
            
            if (cmDTO is not null)
            {
                var model = _mapper.Map<UpdateCMVM>(cmDTO);
                return View(model);
            }
            ViewData["Error"] = "Kişi bulunamadı!";
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCM(UpdateCMVM model)
        {
            if (ModelState.IsValid)
            {
                var cmDTO = _mapper.Map<UpdateCMDTO>(model);
                var result = await _cmService.UpdateAsync(cmDTO);
                if (result)
                {
                    TempData["Success"] = "Müşteri yöneticisi güncellendi!";
                    return RedirectToAction("Index");
                }
                TempData["Error"] = "Müşteri yönecisi güncellenemedi!";
                return View(model);
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        public async Task<IActionResult> DeleteCM(string id)
        {
            Guid entityID;
            var guidResult = Guid.TryParse(id, out entityID);
            if (!guidResult)
            {
                TempData["Error"] = "Müşteri Yöneticisi bulunamamıştır!";
                return RedirectToAction("Index");
            }

            var cmDTO = await _cmService.GetByIdAsync<GetCMDTO>(entityID);
            
            if (cmDTO is not null)
            {
                var result = await _cmService.DeleteAsync(cmDTO);
                if (result)
                {
                    TempData["Success"] = "Müşteri yönetici silinmiştir!";
                    return RedirectToAction("Index");
                }
                TempData["Error"] = "Müşteri yöneticisi silinemedi!";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Müşteri yöneticisi bulunamadı!";
            return RedirectToAction("Index");
        }

    }
}
