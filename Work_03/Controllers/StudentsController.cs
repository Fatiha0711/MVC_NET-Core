using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Work_03.Model;
using Work_03.Model.ViewModels;

namespace Work_03.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public StudentsController(StudentDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.Include(x => x.StudentSubjects).ThenInclude(y => y.Subjects).ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult AddNewSubjects(int? id)
        {
            ViewBag.subject = new SelectList(_context.Subjects, "SubjectId", "SubjectName", id.ToString() ?? "");
            return PartialView("_addNewSubjects");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentVM studentVM, int[] subjectId)
        {
            if (ModelState.IsValid)
            {
                Student student = new Student()
                {
                    StudentName = studentVM.StudentName,
                    DateOfBirth = studentVM.DateOfBirth,
                    Phone = studentVM.Phone,
                    MorningShift = studentVM.MorningShift
                };
                //For Image
                var file = studentVM.ImageFile;
                string webroot = _environment.WebRootPath;
                string folder = "Images";
                string imgFileName = Path.GetFileName(studentVM.ImageFile.FileName);
                string fileToSave = Path.Combine(webroot, folder, imgFileName);
                if (file != null)
                {
                    using (var stream = new FileStream(fileToSave, FileMode.Create))
                    {
                        studentVM.ImageFile.CopyTo(stream);
                        student.Image = "/" + folder + "/" + imgFileName;
                    }
                }
                foreach (var item in subjectId)
                {
                    StudentSubject studentSubject = new StudentSubject()
                    {
                        Students = student,
                        StudentId = student.StudentId,
                        SubjectId = item
                    };
                    _context.StudentSubjects.Add(studentSubject);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.StudentId == id);
            StudentVM studentVM = new StudentVM()
            {
                StudentId = student.StudentId,
                StudentName = student.StudentName,
                DateOfBirth = student.DateOfBirth,
                Phone = student.Phone,
                Image = student.Image,
                MorningShift = student.MorningShift
            };
            var existSubject = _context.StudentSubjects.Where(x => x.StudentId == id).ToList();
            foreach (var item in existSubject)
            {
                studentVM.SubjectList.Add(item.SubjectId);
            }
            return View(studentVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(StudentVM studentVM, int[] subjectId)
        {
            if (ModelState.IsValid)
            {
                Student student = new Student()
                {
                    StudentId = studentVM.StudentId,
                    StudentName = studentVM.StudentName,
                    DateOfBirth = studentVM.DateOfBirth,
                    Phone = studentVM.Phone,
                    MorningShift = studentVM.MorningShift,
                    Image = studentVM.Image
                };
                var file = studentVM.ImageFile;
                if (file != null)
                {
                    string webroot = _environment.WebRootPath;
                    string folder = "Images";
                    string imgFileName = Path.GetFileName(studentVM.ImageFile.FileName);
                    string fileToSave = Path.Combine(webroot, folder, imgFileName);
                    using (var stream = new FileStream(fileToSave, FileMode.Create))
                    {
                        studentVM.ImageFile.CopyTo(stream);
                        student.Image = "/" + folder + "/" + imgFileName;
                    }
                }
                else
                {
                    student.Image = studentVM.Image;
                }

                var existSubject = _context.StudentSubjects.Where(x => x.StudentId == student.StudentId).ToList();
                foreach (var item in existSubject)
                {
                    _context.StudentSubjects.Remove(item);
                }
                foreach (var item in subjectId)
                {
                    StudentSubject studentSubject = new StudentSubject()
                    {

                        StudentId = student.StudentId,
                        SubjectId = item
                    };
                    _context.StudentSubjects.Add(studentSubject);
                }
                _context.Update(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                var stdnt = _context.Students.FirstOrDefault(x => x.StudentId == id);
                var studentSubject = _context.StudentSubjects.FirstOrDefault(x => x.StudentId == id);

                _context.StudentSubjects.Remove(studentSubject);
                _context.Students.Remove(stdnt);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Index");
        }
    }
}
