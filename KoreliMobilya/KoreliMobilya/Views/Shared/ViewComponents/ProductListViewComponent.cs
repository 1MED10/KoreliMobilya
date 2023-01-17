using KoreliMobilyaDeneme.Models;
using Microsoft.AspNetCore.Mvc;

namespace KoreliMobilyaDeneme.Views.Shared.ViewComponents
{
    public class ProductListViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public ProductListViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var viewmodels = _context.Products.Select(x => new
            ProductListComponentViewModel()
            {
                Name = x.Name,
                ImagePath=x.ImagePath,
                ImagePath2 = x.ImagePath2,
                ImagePath3= x.ImagePath3,
                CategoryId=x.CategoryId,
               
            }).ToList();

            return View(viewmodels);

        }
    }
}
