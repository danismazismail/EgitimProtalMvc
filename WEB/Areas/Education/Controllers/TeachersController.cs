using ApplicationCore.Consts;
using AutoMapper;
using Business.Managers.Interface;
using DTO.Concrete.Clasrooms;
using DTO.Concrete.Students;
using DTO.Concrete.Teachers;
using DTO.Concrete.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using WEB.Areas.Education.Models.ViewModels.Classrooms;
using WEB.Areas.Education.Models.ViewModels.Students;
using WEB.Areas.Education.Models.ViewModels.Teachers;

namespace WEB.Controllers
{
    [Area("Education")]
    public class TeachersController : Controller
    {
        private readonly ITeacherManager _teacherManager;
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IClassroomManager _classroomManager;
        private readonly IStudentManager _studentManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TeachersController(ITeacherManager teacherManager, IMapper mapper, IUserManager userManager, IEmailSender emailSender, IClassroomManager classroomManager, IStudentManager studentManager, IWebHostEnvironment webHostEnvironment)
        {
            _teacherManager = teacherManager;
            _mapper = mapper;
            _userManager = userManager;
            _emailSender = emailSender;
            _classroomManager = classroomManager;
            _studentManager = studentManager;
            _webHostEnvironment = webHostEnvironment;
        }

        [Authorize(Roles = "admin, customerManager")]
        public async Task<IActionResult> Index()
        {
            var teachers = await _teacherManager.GetFilteredListAsync
                (
                    select: x => new GetTeacherVM
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Email = x.Email,
                        HireDate = x.HireDate,
                        BirthDate = x.BirthDate,
                        Course = x.Course,
                        CreatedDate = x.CreatedDate,
                        UpdatedDate = x.UpdatedDate,
                        Status = x.Status
                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderByDescending(z => z.CreatedDate)
                );

            return View(teachers);
        }

        [Authorize(Roles = "admin, customerManager")]
        public IActionResult CreateTeacher() => View(new CreateTeacherVM());


        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, customerManager")]
        public async Task<IActionResult> CreateTeacher(CreateTeacherVM model)
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
                        TempData["Error"] = "Öğretmen bulunamadı!";
                        return View(model);
                    }

                    var resultRole = await _userManager.AddUserToRole(appUserDto.Email, "teacher");
                    if (resultRole)
                    {
                        var dto = _mapper.Map<CreateTeacherDTO>(model);
                        dto.AppUserId = appUser.Id;

                        var result = await _teacherManager.AddAsync(dto);

                        if (result)
                        {
                            var token = await _userManager.GenerateTokenForResetPassword(appUser.Id);

                            var callBackURL = Url.Action("CreatePassword", "Account", new { token, email = appUser.Email }, Request.Scheme);

                            await _emailSender.SendEmailAsync(appUser.Email, "Yeni Kayıt Oluşturma İçin Şifre Değişikliği",
                                $"<p>Kullanıcı Adınız: {appUser.UserName}</p><br /><p>Şifreniz: 1234</p><br /><p>Lütfen şifrenizi sıfırlamak için <a href='{callBackURL}'>buraya tıklayınız!</a></p>");

                            TempData["Success"] = $"Öğretmen sisteme kaydedilmiştir! Giriş bilgileri {appUser.Email} adresine gönderilmiştir!";
                            return RedirectToAction("Index");
                        }
                        TempData["Error"] = "Öğretmen sisteme kaydedilememiştir!";
                        return View(model);
                    }
                }
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        [Authorize(Roles = "admin, customerManager")]
        public async Task<IActionResult> UpdateTeacher(string id)
        {
            Guid entityID;
            var guidResult = Guid.TryParse(id, out entityID);
            if (!guidResult)
            {
                TempData["Error"] = "Öğretmen bulunamamıştır!";
                return RedirectToAction("Index");
            }

            var dto = await _teacherManager.GetByIdAsync<UpdateTeacherDTO>(entityID);

            if (dto != null)
            {
                var model = _mapper.Map<UpdateTeacherVM>(dto);
                return View(model);
            }
            TempData["Error"] = "Öğretmen bulunamadı!";
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, customerManager")]
        public async Task<IActionResult> UpdateTeacher(UpdateTeacherVM model)
        {
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<UpdateTeacherDTO>(model);
                var result = await _teacherManager.UpdateAsync(dto);
                if (result)
                {
                    TempData["Success"] = "Öğretmen güncellenmiştir!";
                    return RedirectToAction("Index");
                }
                TempData["Error"] = "Öğretmen güncellenememiştir!";
                return View(model);
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        [Authorize(Roles = "admin, customerManager")]
        public async Task<IActionResult> DeleteTeacher(string id)
        {
            Guid entityID;
            var guidResult = Guid.TryParse(id, out entityID);
            if (!guidResult)
            {
                TempData["Error"] = "Öğretmen bulunamamıştır!";
                return RedirectToAction("Index");
            }

            var dto = await _teacherManager.GetByIdAsync<GetTeacherDTO>(entityID);

            if (dto != null)
            {
                var result = await _teacherManager.DeleteAsync(dto);
                if (result)
                {
                    TempData["Success"] = "Öğretmen silinmiştir!";
                    return RedirectToAction("Index");
                }
                TempData["Error"] = "Öğretmen silinememiştir!";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Öğretmen bulunamamıştır";
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "teacher")]
        public async Task<IActionResult> GetClassrooms()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {

                var teacherId = await _userManager.GetUserId(HttpContext.User);

                var classrooms = await _classroomManager.GetFilteredListAsync
                    (
                        select: x => new GetClassroomsForTeacherDTO
                        {
                            Id = x.Id,
                            ClassroomName = x.ClassroomName,
                            Description = x.Description,
                            ClassroomSize = x.Students.Count()
                        },
                        where: x => x.Teacher.AppUserID == teacherId,
                        orderBy: x => x.OrderByDescending(z => z.CreatedDate),
                        join: x => x.Include(z => z.Students).Include(z => z.Teacher)

                    );
                if (classrooms != null)
                {
                    var model = _mapper.Map<List<GetClassroomsForTeacherVM>>(classrooms);
                    return View(model);
                }

                TempData["Error"] = "Sınıf bulunamadı!";
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [Authorize(Roles = "teacher")]
        public async Task<IActionResult> GetClassroom(string id)
        {
            Guid entityID;
            var guidResult = Guid.TryParse(id, out entityID);
            if (!guidResult)
            {
                TempData["Error"] = "Sınıf bulunamamıştır!";
                return RedirectToAction("Index");
            }

            var students = await _studentManager.GetFilteredListAsync
                (
                    select: x => new GetStudentsForClassroomDTO
                    {
                        Id = x.Id,
                        FullName = x.FirstName + " " + x.LastName,
                        Exam1 = x.Exam1,
                        Exam2 = x.Exam2,
                        ProjectExam = x.ProjectExam,
                        Average = x.Average,
                        ProjectName = x.ProjectName,
                        ProjectPath = x.ProjectPath,
                        StudentStatus = x.StudentStatus,

                    },
                    where: x => x.ClassroomId == entityID,
                    orderBy: x => x.OrderByDescending(z => z.FirstName + " " + z.LastName)
                );

            if (students != null)
            {
                var model = _mapper.Map<List<GetStudentsForClassroomVM>>(students);
                return View(model);
            }
            TempData["Error"] = "Sınıf bulunamadı!";
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "teacher")]
        public async Task<IActionResult> GetStudentDetail(string id)
        {
            Guid entityID;
            var guidResult = Guid.TryParse(id, out entityID);
            if (!guidResult)
            {
                TempData["Error"] = "Öğrenci bulunamamıştır!";
                return RedirectToAction("Index");
            }
            var student = await _studentManager.GetByIdAsync<StudentDetailForExamsDTO>(entityID);

            if (student != null)
            {
                var model = _mapper.Map<StudentDetailForExamsVM>(student);
                var classroom = await _classroomManager.GetByIdAsync<GetClassroomDTO>(model.ClassroomId);
                model.ClassroomName = classroom.ClassroomName;
                return View(model);
            }

            TempData["Error"] = "Öğrenci bulunamamıştır!";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "teacher")]
        public async Task<IActionResult> GetStudentDetail(StudentDetailForExamsVM model)
        {
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<StudentDetailForExamsDTO>(model);
                var result = await _studentManager.UpdateAsync(dto);
                if (result)
                {
                    TempData["Success"] = "Öğrenci notu girilmiştir!";
                    return RedirectToAction("GetClassroom", new { id = dto.ClassroomId });
                }
                TempData["Error"] = "Öğrenci notu girilememiştir!";
                return View(model);
            }
            TempData["Error"] = "Lütfen aşiağıdaki kurallara uyunuz!";
            return View(model);
        }

        [Authorize(Roles = "teacher")]
        public FileResult Download(string filePath)
        {
            string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "projects/");
            byte[] fileBytes = System.IO.File.ReadAllBytes(uploadDir + filePath);
            string fileName = filePath;
            //System.Net.Mime.MediaTypeNames.Application.Octet: Gelen dosyanın türü belli olmadığı zaman kullanılır.
            //File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName); Dosya indirme işlemi için kullanılır.
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}
