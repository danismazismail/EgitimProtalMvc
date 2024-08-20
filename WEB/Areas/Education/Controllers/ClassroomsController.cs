using ApplicationCore.Consts;
using AutoMapper;
using Business.Managers.Interface;
using DTO.Concrete.Clasrooms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEB.Areas.Education.Models.ViewModels.Classrooms;
using WEB.Areas.Education.Models.ViewModels.Teachers;

namespace WEB.Areas.Education.Controllers
{
    [Area("Education")]
    [Authorize(Roles = "admin, customerManager")]
    public class ClassroomsController : Controller
    {
        private readonly IClassroomManager _classroomManager;
        private readonly IMapper _mapper;
        private readonly ITeacherManager _teacherManager;

        public ClassroomsController(IClassroomManager classroomManager, IMapper mapper, ITeacherManager teacherManager)
        {
            _classroomManager = classroomManager;
            _mapper = mapper;
            _teacherManager = teacherManager;
        }

        private async Task<List<SelectListItem>> GetTeachers(Guid? teacherId)
        {
            var teachers = await _teacherManager.GetFilteredListAsync
             (
                 select: x => new GetTeachersForClassroomsVM
                 {
                     Id = x.Id,
                     FullName = x.FirstName + " " + x.LastName
                 }
             );

            var selectlistTeachers = teachers.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.FullName,
                Selected = teacherId != null ? x.Id == teacherId : false
            }).ToList();

            return selectlistTeachers;
        }

        public async Task<IActionResult> Index()
        {
            var classrooms = await _classroomManager.GetFilteredListAsync
                (
                    select: x => new GetClassroomVM
                    {
                        Id = x.Id,
                        ClassroomName = x.ClassroomName,
                        Description = x.Description,
                        ClassroomSize = x.Students.Where(x => x.Status != Status.Passive).ToList().Count,
                        TeacherName = x.Teacher.FirstName + " " + x.Teacher.LastName,
                        CreatedDate = x.CreatedDate,
                        UpdatedDate = x.UpdatedDate,
                        Status = x.Status
                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderByDescending(z => z.CreatedDate),
                    join: x => x.Include(z => z.Teacher).Include(z => z.Students)
                );

            return View(classrooms);
        }

        public async Task<IActionResult> CreateClassroom() => View(new CreateClassroomVM { Teachers = await GetTeachers(null) });

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateClassroom(CreateClassroomVM model)
        {

            model.Teachers = await GetTeachers(null);
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<CreateClassroomDTO>(model);
                var result = await _classroomManager.AddAsync(dto);
                if (result)
                {
                    TempData["Success"] = "Sınıf eklendi!";
                    return RedirectToAction("Index");
                }
                TempData["Error"] = "Sınıf eklenemedi!";
                return View(model);
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        public async Task<IActionResult> UpdateClassroom(string id)
        {
            Guid entityID;
            var guidResult = Guid.TryParse(id, out entityID);
            if (!guidResult)
            {
                TempData["Error"] = "Sınıf bulunamamıştır!";
                return RedirectToAction("Index");
            }

            var dto = await _classroomManager.GetByIdAsync<UpdateClassroomDTO>(Guid.Parse(id));

            if (dto != null)
            {
                var model = _mapper.Map<UpdateClassroomVM>(dto);
                model.Teachers = await GetTeachers(model.TeacherId);

                return View(model);
            }

            TempData["Error"] = "Sınıf bulunamamıştır!";
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateClassroom(UpdateClassroomVM model)
        {
            model.Teachers = await GetTeachers(model.TeacherId);

            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<UpdateClassroomDTO>(model);
                var result = await _classroomManager.UpdateAsync(dto);

                if (result)
                {
                    TempData["Success"] = "Sınıf güncellendi!";
                    return RedirectToAction("Index");
                }
                TempData["Error"] = "Sınıf güncellenemedi!";
                return View(model);
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        public async Task<IActionResult> DeleteClassroom(string id)
        {
            Guid entityID;
            var guidResult = Guid.TryParse(id, out entityID);
            if (!guidResult)
            {
                TempData["Error"] = "Sınıf bulunamamıştır!";
                return RedirectToAction("Index");
            }

            var dto = await _classroomManager.GetByIdAsync<GetClassroomDTO>(entityID);
            if (dto != null)
            {
                var result = await _classroomManager.DeleteAsync(dto);
                if (result)
                {
                    TempData["Success"] = "Sınıf silindi!";
                    return RedirectToAction("Index");
                }
                TempData["Error"] = "Sınıf silinemedi!";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Sınıf bulunumadı!";
            return RedirectToAction("Index");
        }

        
    }
}
