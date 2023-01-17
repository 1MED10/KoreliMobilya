using KoreliMobilyaDeneme.Models;
using KoreliMobilyaDeneme.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KoreliMobilyaDeneme.Views.Shared.ViewComponents
{
    public class BannerListViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;

        public BannerListViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var viewmodelss = _context.Banners.Select(x => new
            BannerListComponentViewModel()
            {
                Name = x.Name,
              ImagePath4=x.ImagePath4,
              Link = x.Link,
               
            }).ToList();

            return View(viewmodelss);



        }

    }
}
