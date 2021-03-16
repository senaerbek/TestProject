using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestProject.Database;
using TestProject.Models;

namespace TestProject.Controllers
{
    [Authorize(Roles="Admin")]
    [ApiController]
    [Route("api/[controller]")]
    
    public class GraphicController : ControllerBase
    {

        private AppDbContext _context;
        public GraphicController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
         public ActionResult<List<Fiyat>> get(int id)
        {
            var marka = _context.Fiyat.Where(z=>z.Urun.UrunId == id).ToList();
            return marka;
        }
    }
}