using Fiorello.DAL;
using Fiorello.Models;
using Fiorello.ViewModels.VMCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorello.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class CategoryController : Controller
    {
        private AppDbContext _context { get; }
        private IEnumerable<Category> categories;
        public CategoryController(AppDbContext context)
        {
            _context = context;
            categories = _context.Categories.Where(ct => !ct.IsDeleted);
        }
        public IActionResult Index()
        { 
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateVM category)
        {
            if (!ModelState.IsValid) return View();
            bool isExist = false;
            foreach (var ct in categories)
            {
                if (category.Name.ToLower() == ct.Name.ToLower())
                {
                    isExist = true; break;
                }
            }
            if (isExist)
            {
                ModelState.AddModelError("Name", $"{category.Name} is exist");
                return View();
            }
            Category newCategory = new Category
            {
                Name = category.Name
            };
            await _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
