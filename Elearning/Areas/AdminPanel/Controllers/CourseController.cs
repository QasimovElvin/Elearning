using Elearning.DAL;
using Elearning.Models;
using Elearning.Utilities.Extensions;
using Elearning.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Elearning.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CourseController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index(int page=1,int take=3)
        {
            var service = _context.Courses.Include(x => x.Teacher).Skip((page-1)*take).Take(take).ToList();
            PaginateVM<Courses> course = new PaginateVM<Courses>()
            {
                Items = service,
                PageCount = PageCaount(take),
                CurrentPage = page,
            };
            return View(course);
        }
        private int PageCaount(int take)
        {
            var result = _context.Courses.Count();
            return (int)Math.Ceiling((decimal)result / take);
        }
        public IActionResult Create()
        {
            ViewBag.Teacher = _context.Teachers.ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Courses courses)
        {
            ViewBag.Teacher = _context.Teachers.ToList();
            if (!ModelState.IsValid) return View();
            if (!courses.ImageFile.CheckType("image/"))
            {
                ModelState.AddModelError("", "Image type invalid");
                return View();
            }
            if (courses.ImageFile.CheckSize(500))
            {
                ModelState.AddModelError("", "Image size wrong");
                return View();
            }
            Courses user=new Courses()
            {
                Name = courses.Name,
                Price = courses.Price,
                TeacherId = courses.TeacherId,
            };
            user.Image = await courses.ImageFile.SaveFileAsync(_env.WebRootPath, "assets/img");
            await _context.Courses.AddAsync(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Teacher = await _context.Teachers.ToListAsync();

            Courses? courses = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);
            return View(courses);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Courses courses)
        {
            ViewBag.Teacher =await _context.Teachers.ToListAsync();
            if (!ModelState.IsValid) return View();
            Courses? exists =await _context.Courses.FirstOrDefaultAsync(x => x.Id == courses.Id);
            if(exists == null)
            {
                ModelState.AddModelError("", "Course is null");
                return View();
            }
            if (courses.ImageFile != null)
            {
                if (!courses.ImageFile.CheckType("image/"))
                {
                    ModelState.AddModelError("", "Image type invalid");
                    return View();
                }
                if (courses.ImageFile.CheckSize(500))
                {
                    ModelState.AddModelError("", "Image size wrong");
                    return View();
                }
                string path = Path.Combine(_env.WebRootPath,"assets/img",exists.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                exists.Image = await courses.ImageFile.SaveFileAsync(_env.WebRootPath, "assets/img");
            }
                exists.Name = courses.Name;
                exists.Price = courses.Price;
                exists.TeacherId = courses.TeacherId;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            Courses? courses = _context.Courses.FirstOrDefault(x => x.Id == id);
            if(courses == null)
            {
                ModelState.AddModelError("", "course is null");
                return View();
            }
            string path = Path.Combine(_env.WebRootPath, "assets/img",courses.Image);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.Remove(courses);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}
