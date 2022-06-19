using Fiorello.DAL;
using Fiorello.Models;
using Fiorello.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorello.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context { get; }
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int pg=1)
        {
            var products = _context.Products.Where(c => !c.IsDeleted)
                .Include(p => p.Images).Include(p => p.Category).ToList();
            const int pageSize = 8;
            if (pg < 1)
            
                pg = 1;
            
            int recsCount = products.Count();
            var pager = new Pager(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;
            var data = products.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;


            HomeViewModel home = new HomeViewModel
            {
                Slides = _context.Slides.ToList(),
                Summary = _context.Summary.FirstOrDefault(),
                Categories = _context.Categories.Where(c => !c.IsDeleted).ToList(),
                Products = data,
                Settings = _context.Settings.FirstOrDefault()
            };

           


            return View(home);
        }
    }
}
