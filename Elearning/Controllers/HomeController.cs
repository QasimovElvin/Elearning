using Elearning.DAL;
using Elearning.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Elearning.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM VM = new HomeVM()
            {
                Abouts = _context.Abouts.Take(4).ToList(),
                Courses=_context.Courses.Include(x=>x.Teacher).Take(4).ToList(),
                Teachers=_context.Teachers.Take(4).ToList(),
            };
            return View(VM);
        }

       
    }
}