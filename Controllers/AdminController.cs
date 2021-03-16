using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestProject.Database;

namespace TestProject.Controllers
{
     [Authorize(Roles="Admin")]
    public class AdminController : Controller
    {
        private AppDbContext _context;
        public AdminController(AppDbContext context)
        {
            _context = context;

        }

        public IActionResult Index()
        {
            var marka = _context.Urun.Include(x=>x.Marka).ToList();
            return View(marka);
        }

    }
}