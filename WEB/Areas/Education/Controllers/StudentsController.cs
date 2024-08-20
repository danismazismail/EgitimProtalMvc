using ApplicationCore.Consts;
using ApplicationCore.Entities.Concrete;
using AutoMapper;
using Business.Managers.Interface;
using DTO.Concrete.Students;
using DTO.Concrete.Users;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEB.Areas.Education.Models.ViewModels.Students;
using WEB.Areas.Education.Models.ViewModels.Classrooms;
using DTO.Concrete.Clasrooms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace WEB.Areas.Education.Controllers
{
    [Area("Education")]
    public class StudentsController : Controller
    {
        private readonly IStudentManager _studentManager;
        private readonly IMapper _mapper;
        private readonly IClassroomManager _classroomManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUserManager _userManager;
        private readonly IEmailSender _emailSender;

        public StudentsController(IStudentManager studentManager, IMapper mapper, IClassroomManager classroomManager, IWebHostEnvironment webHostEnvironment, IUserManager userManager, IEmailSender emailSender)
        {
            _studentManager = studentManager;
            _mapper = mapper;
            _classroomManager = classroomManager;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _emailSender = emailSender;
        }


        private async Task<List<SelectListItem>> GetClassrooms(Guid? classroomId)
        {
            var classrooms = await _classroomManager.GetFilteredListAsync
                (
                    select: x => new GetClassroomsForStudentsVM
                    {
                        Id = x.Id,
                        ClassroomName = x.ClassroomName,
                        TeacherName = x.Teacher.FirstName + " " + x.Teacher.LastName,
                        ClassroomSize = x.Students.Where(x => x.Status != Status.Passive).ToList().Count
                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderByDescending(z => z.CreatedDate),
                    join: x => x.Include(z => z.Teacher).Include(z => z.Students)
            );

            var selectlistClassrooms = classrooms.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = $"{x.ClassroomName} ({x.ClassroomSize}) - {x.TeacherName}",
                // YZL-8150 (15) - Sina Emre BEKAR
                Selected = classroomId != null ? x.Id == classroomId : false
            }).ToList();

            return selectlistClassrooms;
        }

        [Authorize(Roles = "admin, customerManager")]
        public async Task<IActionResult> Index()
        {
            var students = await _studentManager.GetFilteredListAsync
                (
                    select: x => new GetStudentVM
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        BirthDate = x.BirthDate,
                        Email = x.Email,
                        ClassroomName = x.Classroom.ClassroomName,
                        TeacherName = x.Classroom.Teacher.FirstName + " " + x.Classroom.Teacher.LastName,
                        Average = x.Average,
                        Status = x.Status,
                        StudentStatus = x.StudentStatus,
                        CreatedDate = x.CreatedDate,
                        UpdatedDate = x.UpdatedDate
                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderByDescending(z => z.CreatedDate),
                    join: x => x.Include(z => z.Classroom).ThenInclude(z => z.Teacher)
                );

            return View(students);
        }

        [Authorize(Roles = "admin, customerManager")]
        public async Task<IActionResult> CreateStudent()
            => View(new CreateStudentVM { Classrooms = await GetClassrooms(null) });

        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, customerManager")]
        public async Task<IActionResult> CreateStudent(CreateStudentVM model)
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
                        TempData["Error"] = "Öğrenci bulunamadı!";
                        return View(model);
                    }
                    var resultRole = await _userManager.AddUserToRole(appUserDto.Email, "student");
                    if (resultRole)
                    {
                        string imageName = "noimage.jpg";

                        if (model.Image != null)
                        {
                            string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                            imageName = $"{Guid.NewGuid()}_{model.FirstName}_{model.LastName}_{model.Image.FileName}";
                            string filePath = Path.Combine(uploadDir, imageName);
                            FileStream fileStream = new FileStream(filePath, FileMode.Create);
                            await model.Image.CopyToAsync(fileStream);
                            fileStream.Close();
                        }

                        var dto = _mapper.Map<CreateStudentDTO>(model);
                        dto.ImagePath = imageName;
                        dto.AppUserId = appUser.Id;

                        var result = await _studentManager.AddAsync(dto);
                        if (result)
                        {
                            var token = await _userManager.GenerateTokenForResetPassword(appUser.Id);

                            var callBackURL = Url.Action("CreatePassword", "Account", new { token, email = appUser.Email }, Request.Scheme);

                            await _emailSender.SendEmailAsync(appUser.Email, "Yeni Kayıt Oluşturma İçin Şifre Değişikliği",
                                $"<p>Kullanıcı Adınız: {appUser.UserName}</p><br /><p>Şifreniz: 1234</p><br /><p>Lütfen şifrenizi sıfırlamak için <a href='{callBackURL}'>buraya tıklayınız!</a></p>");

                            TempData["Success"] = $"Öğrenci sisteme kaydedilmiştir! Giriş bilgileri {appUser.Email} adresine gönderilmiştir!";
                            return RedirectToAction("Index");
                        }
                        TempData["Error"] = "Öğrenci sisteme kayıt edilememiştir!";
                        return View(model);
                    }

                }


            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "admin, customerManager")]
        public async Task<IActionResult> UpdateStudent(string id)
        {
            Guid entityID;
            var guidResult = Guid.TryParse(id, out entityID);
            if (!guidResult)
            {
                TempData["Error"] = "Öğrenci bulunamamıştır!";
                return RedirectToAction("Index");
            }

            var dto = await _studentManager.GetByIdAsync<UpdateStudentDTO>(entityID);
            if (dto is not null)
            {
                var model = _mapper.Map<UpdateStudentVM>(dto);
                model.Classrooms = await GetClassrooms(model.ClassroomId);
                return View(model);
            }
            TempData["Error"] = "Öğrenci bulunamadı!";
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, customerManager")]
        public async Task<IActionResult> UpdateStudent(UpdateStudentVM model)
        {
            model.Classrooms = await GetClassrooms(model.ClassroomId);

            if (ModelState.IsValid)
            {
                var imageName = model.ImagePath;

                if (model.Image is not null)
                {
                    string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "img");

                    //Eski Resmi Silme Bölümü
                    if (model.ImagePath != null && !string.Equals(model.ImagePath, "noimage.jpg"))
                    {
                        string oldPath = Path.Combine(uploadDir, model.ImagePath);
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }

                    //Yeni Resim Yükleme Bölümü
                    imageName = $"{Guid.NewGuid()}_{model.FirstName}_{model.LastName}_{model.Image.FileName}";
                    string filePath = Path.Combine(uploadDir, imageName);
                    FileStream fileStream = new FileStream(filePath, FileMode.Create);
                    await model.Image.CopyToAsync(fileStream);
                    fileStream.Close();
                }

                var dto = _mapper.Map<UpdateStudentDTO>(model);
                dto.ImagePath = imageName;

                var result = await _studentManager.UpdateAsync(dto);
                if (result)
                {
                    TempData["Success"] = "Öğrenci güncellendi!";
                    return RedirectToAction("Index");
                }
                TempData["Error"] = "Öğrenci güncellenemedi!";
                return View(model);
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        [Authorize(Roles = "admin, customerManager")]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            Guid entityID;
            var guidResult = Guid.TryParse(id, out entityID);
            if (!guidResult)
            {
                TempData["Error"] = "Öğrenci bulunamamıştır!";
                return RedirectToAction("Index");
            }

            var dto = await _studentManager.GetByIdAsync<GetStudentDTO>(entityID);
            if (dto != null)
            {
                var result = await _studentManager.DeleteAsync(dto);
                if (result)
                {
                    TempData["Success"] = "Öğrenci silinmiştir!";
                    return RedirectToAction("Index");
                }
                TempData["Error"] = "Öğrenci silinememiştir!";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Öğrenci bulunamamıştır!";
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "student")]
        public async Task<IActionResult> StudentDetail()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var studentId = await _userManager.GetUserId(HttpContext.User);
                var dto = await _studentManager.GetByDefaultAsync<StudentDetailForProjectDTO>(x => x.AppUserID == studentId);
                if (dto != null)
                {
                    var model = _mapper.Map<StudentDetailForProjectVM>(dto);
                    var classroom = await _classroomManager.GetByIdAsync<GetClassroomDTO>(dto.ClassroomId);
                    model.ClassroomName = classroom.ClassroomName;
                    return View(model);
                }
                TempData["Error"] = "Öğrenci bulunamadı!";
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "student")]
        public async Task<IActionResult> SendProject(StudentDetailForProjectVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.Project != null)
                {
                    var uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "projects");
                    string fileName = $"{Guid.NewGuid()}_{model.FirstName}_{model.LastName}_{model.Project.FileName}";
                    string filePath = Path.Combine(uploadDir, fileName);

                    FileStream fileStream = new FileStream(filePath, FileMode.Create);
                    await model.Project.CopyToAsync(fileStream);
                    fileStream.Close();

                    var dto = _mapper.Map<StudentDetailForProjectDTO>(model);
                    dto.ProjectPath = fileName;


                    var result = await _studentManager.UpdateAsync(dto);
                    if (result)
                    {
                        TempData["Success"] = "Proje yüklenmiştir!";
                        return RedirectToAction("StudentDetail");
                    }
                    TempData["Error"] = "Proje yüklenememiştir!";
                    return RedirectToAction("StudentDetail");
                }
                return RedirectToAction("StudentDetail");
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return RedirectToAction("StudentDetail");
        }

        [Authorize(Roles = "student")]
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
