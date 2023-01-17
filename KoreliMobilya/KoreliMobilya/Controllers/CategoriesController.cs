using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KoreliMobilyaDeneme.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.FileProviders;
using KoreliMobilyaDeneme.ViewModels;
using AutoMapper;

namespace KoreliMobilyaDeneme.Controllers
{
   
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IFileProvider _fileProvider;
        private readonly IMapper _mapper;
        public CategoriesController(AppDbContext context, IFileProvider fileProvider, IMapper mapper)
        {
            _context = context;
            _fileProvider = fileProvider;
            _mapper = mapper;
        }

        //[Authorize]
        // GET: Categories
        public async Task<IActionResult> Index()
        {

              return View(await _context.Category.ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Category == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(CategoryViewModel newCategory)
        {
            IActionResult result = null;
            if (ModelState.IsValid)
            {
                try
                {
                    var category = _mapper.Map<Category>(newCategory);
                  
                    if (newCategory.Image5 != null && newCategory.Image5.Length>0)
                    {
                        var root = _fileProvider.GetDirectoryContents("wwwroot");
                        var images = root.First(x => x.Name == "Images");
                        var randomImageName = Guid.NewGuid() + Path.GetExtension(newCategory.Image5.FileName);
                        var path = Path.Combine(images.PhysicalPath, randomImageName);
                        using var stream = new FileStream(path, FileMode.Create);
                        newCategory.Image5.CopyTo(stream);
                        category.ImagePath5 = randomImageName;
                    }
                    _context.Category.Add(category);
                    _context.SaveChanges();
                    TempData["status"] = "Ürün Başarıyla Eklendi.";
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    result = View();
                }
            }
            else
            {
                result = View();
            }

            return result;
        }


        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var category = _context.Category.Find(id);
            return View(_mapper.Map<CategoryViewModel>(category));
        }



       
        [HttpPost]
        public IActionResult Edit(CategoryViewModel updateCategory)
        {
            if (updateCategory.Image5 != null && updateCategory.Image5.Length > 0)
            {
                var root = _fileProvider.GetDirectoryContents("wwwroot");
                var images = root.First(x => x.Name == "Images");
               var randomImageName = Guid.NewGuid() + Path.GetExtension(updateCategory.Image5.FileName);
               var path = Path.Combine(images.PhysicalPath, randomImageName);
                using var stream = new FileStream(path, FileMode.Create);
                updateCategory.Image5.CopyTo(stream);
                updateCategory.ImagePath5 = randomImageName;
            }
            var category = _mapper.Map<Category>(updateCategory);
            _context.Category.Update(category);
            _context.SaveChanges();
            TempData["status"] = "Ürün Başarıyla Güncellendi.";
            return RedirectToAction("Index");


        }

  

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Category == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Category == null)
            {
                return Problem("Entity set 'AppDbContext.Category'  is null.");
            }
            var category = await _context.Category.FindAsync(id);
            if (category != null)
            {
                _context.Category.Remove(category);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
          return _context.Category.Any(e => e.Id == id);
        }
    }
}
