using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fiorello.DAL;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello.Areas.AdminPanel.Controllers
{
    public class SlideController : Controller
    {
        private AppDbContext _context { get; set; }
        public SlideController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
