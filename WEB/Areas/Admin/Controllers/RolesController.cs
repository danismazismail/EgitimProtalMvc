using AutoMapper;
using Business.Managers.Interface;
using DTO.Concrete.Roles;
using DTO.Concrete.Users;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WEB.Areas.Admin.Models.Roles;
using WEB.Areas.Admin.Models.Users;

namespace WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class RolesController : Controller
    {
        private readonly IRoleManager _roleManager;
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;

        public RolesController(IRoleManager roleManager, IMapper mapper, IUserManager userManager)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.GetRolesAsync();
            var model = _mapper.Map<List<GetRoleVM>>(roles);
            return View(model);
        }

        public IActionResult CreateRole() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(CreateRoleVM model)
        {
            if (ModelState.IsValid)
            {

                var checkName = await _roleManager.CheckRoleNameAsync(model.Name, null);
                if (checkName)
                {
                    TempData["Error"] = "Bu rol sistemde vardır!";
                    return View(model);
                }

                var dto = _mapper.Map<CreateRoleDTO>(model);
                var result = await _roleManager.AddRoleAsync(dto);

                if (result)
                {
                    TempData["Success"] = "Rol kaydedildi!";
                    return RedirectToAction("Index");
                }
                TempData["Error"] = "Rol kaydedilemedi!";
                return View(model);
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        public async Task<IActionResult> UpdateRole(string id)
        {
            Guid entityID;
            var guidResult = Guid.TryParse(id, out entityID);
            if (!guidResult)
            {
                TempData["Error"] = "Rol bulunamamıştır!";
                return RedirectToAction("Index");
            }

            var dto = await _roleManager.FindRoleAsync<UpdateRoleDTO>(entityID);

            if (dto == null)
            {
                TempData["Error"] = "Rol bulunamamıştır!";
                return RedirectToAction("Index");
            }

            var model = _mapper.Map<UpdateRoleVM>(dto);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateRole(UpdateRoleVM model)
        {
            if (ModelState.IsValid)
            {
                var checkName = await _roleManager.CheckRoleNameAsync(model.Name, model.Id);
                if (checkName)
                {
                    TempData["Error"] = "Bu isim kullanılmaktadır!";
                    return View(model);
                }
                var dto = _mapper.Map<UpdateRoleDTO>(model);
                var result = await _roleManager.UpdateRoleAsync(dto);

                if (result)
                {
                    TempData["Success"] = "Rol güncellendi!";
                    return RedirectToAction("Index");
                }
                TempData["Error"] = "Rol güncellenemedi!";
                return View(model);
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        public async Task<IActionResult> DeleteRole(string id)
        {
            Guid entityID;
            var guidResult = Guid.TryParse(id, out entityID);
            if (!guidResult)
            {
                TempData["Error"] = "Rol bulunamamıştır!";
                return RedirectToAction("Index");
            }

            var dto = await _roleManager.FindRoleAsync<GetRoleDTO>(entityID);

            if (dto is not null)
            {
                var result = await _roleManager.DeleteRoleAsync((Guid)dto.Id);
                if (result)
                {
                    TempData["Success"] = "Rol silindi!";
                    return RedirectToAction("Index");
                }
                TempData["Error"] = "Rol silinemedi!";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Rol bulunamadı!";
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> AssignRole(string id)
        {
            Guid entityID;
            var guidResult = Guid.TryParse(id, out entityID);
            if (!guidResult)
            {
                TempData["Error"] = "Rol bulunamamıştır!";
                return RedirectToAction("Index");
            }

            var dto = await _roleManager.FindRoleAsync<GetRoleDTO>(entityID);

            if (dto != null)
            {
                var model = new AssignRoleVM
                {
                    Role = _mapper.Map<GetRoleVM>(dto),
                    RoleName = dto.Name
                };
                
                var hasRole = _mapper.Map<List<GetUserForRoleVM>>(await _userManager.GetUsersHasRole(dto.Name));
                var hasNotRole = _mapper.Map<List<GetUserForRoleVM>>(await _userManager.GetUsersHasNotRole(dto.Name));

                if (hasRole != null)
                    model.HasRole = hasRole;
                if (hasNotRole != null)
                    model.HasNotRole = hasNotRole;
                
                return View(model);
            }

            TempData["Error"] = "Rol bulunamadı!";
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRole(AssignRoleVM model)
        {
            model.HasRole = _mapper.Map<List<GetUserForRoleVM>>(await _userManager.GetUsersHasRole(model.RoleName));
            model.HasNotRole = _mapper.Map<List<GetUserForRoleVM>>(await _userManager.GetUsersHasNotRole(model.RoleName));

            bool resultAdd = true, resultRemove = true;

            foreach (var userId in model.AddIds ?? new string[] {})
            {
                var user = await _userManager.FindUser<GetUserForRoleDTO>(Guid.Parse(userId));
                if (user != null)
                    resultAdd = await _userManager.AddUserToRole(user.Id, model.RoleName);
            }

            foreach (var userId in model.RemoveIds ?? new string[] { })
            {
                var user = await _userManager.FindUser<GetUserForRoleDTO>(Guid.Parse(userId));
                if (user != null)
                    resultRemove = await _userManager.RemoveUserFromRole(user, model.RoleName);
            }

            if (resultAdd && resultRemove)
            {
                TempData["Success"] = "Rol işlemleri başarılı bir şekilde gerçekleşti!";
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Rol işlemleri gerçekleştirilemedi!";
            return View(model);
        }
    }
}
