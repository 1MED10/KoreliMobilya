using AutoMapper;
using KoreliMobilyaDeneme.Models;
using KoreliMobilyaDeneme.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using static KoreliMobilyaDeneme.ViewModels.BannerListPartialViewModel;
using static KoreliMobilyaDeneme.ViewModels.CategoryListPartialViewModel;

namespace KoreliMobilyaDeneme.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;


        public HomeController(ILogger<HomeController> logger, AppDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var products = _context.Products.OrderByDescending(x => x.Id).Select(x => new ProductPartialViewModel()
            {
                Id = x.Id,
                Name = x.Name,

            }).ToList();

            var banners = _context.Banners.OrderByDescending(x => x.Id).Select(x => new BannerPartialViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                ImagePath4 = x.ImagePath4,
                Link = x.Link,
            }).ToList();

            var categories = _context.Category.OrderByDescending(x => x.Id).Select(x => new CategoryPartialViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                ImagePath5 = x.ImagePath5
            }).ToList();


            ViewBag.productListPartialViewModel = new BannerListPartialViewModel()
            {
                Banners = banners
            };

            ViewBag.categoryListPartialViewModel = new CategoryListPartialViewModel()
            {
                Categories = categories
            };


            ViewBag.productListPartialViewModel = new ProductListPartialViewModel()
            {
                Products = products
            };



            return View();
        }



        public IActionResult Yatak()
        {

            List<ProductViewModel> products = _context.Products
        .Include(x => x.Category)
        .Where(x => x.Category.Name == "Yatak Odaları")
        .Select(x => new ProductViewModel()
        {
            Id = x.Id,
            Name = x.Name,
            CategoryName = x.Category.Name,
            ImagePath = x.ImagePath,
            ImagePath2 = x.ImagePath2,
            ImagePath3 = x.ImagePath3,
            ImagePath7 = x.ImagePath7,
            Link=x.Link
        }).ToList();





            return View(products);

        }


        public IActionResult Bebevegenc()
        {
            List<ProductViewModel> products = _context.Products
       .Include(x => x.Category)
       .Where(x => x.Category.Name == "Genç Odaları")
       .Select(x => new ProductViewModel()
       {
           Id = x.Id,
           Name = x.Name,
           CategoryName = x.Category.Name,
           ImagePath = x.ImagePath,
           ImagePath2 = x.ImagePath2,
           ImagePath3 = x.ImagePath3,
           ImagePath7 = x.ImagePath7,
           Link = x.Link

       }).ToList();
            return View(products);
        }

        public IActionResult Tv()
        {
            List<ProductViewModel> products = _context.Products
    .Include(x => x.Category)
    .Where(x => x.Category.Name == "Tv Üniteleri")
    .Select(x => new ProductViewModel()
    {
        Id = x.Id,
        Name = x.Name,
        CategoryName = x.Category.Name,
        ImagePath = x.ImagePath,
        ImagePath2 = x.ImagePath2,
        ImagePath3 = x.ImagePath3,
        ImagePath7 = x.ImagePath7,
        Link = x.Link

    }).ToList();
            return View(products);
        }


        public IActionResult Yemek()
        {
            List<ProductViewModel> products = _context.Products
   .Include(x => x.Category)
   .Where(x => x.Category.Name == "Yemek Odaları")
   .Select(x => new ProductViewModel()
   {
       Id = x.Id,
       Name = x.Name,
       CategoryName = x.Category.Name,
       ImagePath = x.ImagePath,
       ImagePath2 = x.ImagePath2,
       ImagePath3 = x.ImagePath3,
       ImagePath7 = x.ImagePath7,
       Link = x.Link

   }).ToList();
            return View(products);
        }


        public IActionResult Mutfak()
        {
            List<ProductViewModel> products = _context.Products
    .Include(x => x.Category)
    .Where(x => x.Category.Name == "Mutfak Dolapları")
    .Select(x => new ProductViewModel()
    {
        Id = x.Id,
        Name = x.Name,
        CategoryName = x.Category.Name,
        ImagePath = x.ImagePath,
        ImagePath2 = x.ImagePath2,
        ImagePath3 = x.ImagePath3,
        ImagePath7 = x.ImagePath7,
        Link = x.Link

    }).ToList();
            return View(products);
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(ErrorViewModel errorViewModel)
        {
            errorViewModel.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            return View(errorViewModel);
        }

        public IActionResult GetById(int id)
        {
            ViewBag.LinkSelect = new SelectList(new List<CategorySelectList>() {

                new(){Data="Yatak Odası", Value="Yatak"},
                new(){Data="Bebe ve Genç Odaları", Value="Bebevegenc"},
                new(){Data="Tv Üniteleri", Value="Tv"},
                new(){Data="Yemek Odaları", Value="Yemek"},
                new(){Data="Mutfak Dolapları", Value="Mutfak"},

            }, "Value", "Data");

            var product = _context.Products.Find(id);
            return View(_mapper.Map<ProductViewModel>(product));


        }

    }
}