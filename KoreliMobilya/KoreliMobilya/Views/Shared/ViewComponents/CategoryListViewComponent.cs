using KoreliMobilyaDeneme.Models;
using KoreliMobilyaDeneme.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KoreliMobilyaDeneme.Views.Shared.ViewComponents
{
    public class CategoryListViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;

        public CategoryListViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var viewmodels = _context.Category.Select(x => new
            CategoryListComponentViewModel()
            {
                Name = x.Name,
                Description = x.Description,
                ImagePath5= x.ImagePath5,

            }).ToList();

            return View(viewmodels);



        }

    }
}
