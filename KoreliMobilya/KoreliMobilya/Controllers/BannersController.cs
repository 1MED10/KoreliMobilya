using AutoMapper;
using KoreliMobilyaDeneme.Models;
using KoreliMobilyaDeneme.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;
using System.Drawing;
using System.Numerics;
using System.Reflection;

namespace KoreliMobilyaDeneme.Controllers
{
    public class BannersController : Controller
    {
        private readonly BannerRepository _bannerRepository;
        private AppDbContext _context;
        private readonly IMapper _mapper;

        private readonly IFileProvider _fileProvider;

        public BannersController(AppDbContext context, IMapper mapper, IFileProvider fileProvider)
        {
            _bannerRepository = new BannerRepository();
            _context = context;
            _mapper = mapper;
            _fileProvider = fileProvider;
        }

        public IActionResult Index()
        {
            var banners = _context.Banners.ToList();
            return View(_mapper.Map<List<BannerViewModel>>(banners));
        }

        public IActionResult Remove(int id)
        {


            var banner = _context.Banners.Find(id);
            //bulduğumuz id yi yanı product sil diyoruz
            _context.Banners.Remove(banner);
            //Ardından veri tabanına kaydetme işlemini gerçekleştiriyoruz.
            _context.SaveChanges();
            return RedirectToAction("Index");

        }



        [HttpGet]

        public IActionResult Add()
        {
            ViewBag.LinkSelect = new SelectList(new List<CategorySelectList>() {

                new(){Data="Yatak Odası", Value="Yatak"},
                new(){Data="Bebe ve Genç Odaları", Value="Bebevegenc"},
                new(){Data="Tv Üniteleri", Value="Tv"},
                new(){Data="Yemek Odaları", Value="Yemek"},
                new(){Data="Mutfak Dolapları", Value="Mutfak"},

            }, "Value", "Data");



            return View();
        }




        [HttpPost]
        //Sacede Product.cs içinden Product alıyoruz yani tiplerini alıyoruz yeterli oluyor
        public IActionResult Add(BannerViewModel newBanner)
        {

            IActionResult result = null;

            if (ModelState.IsValid)
            {
                try
                {

                    var banner = _mapper.Map<Banner>(newBanner);
                    if (newBanner.Image4 != null && newBanner.Image4.Length > 0)
                    {
                        var root = _fileProvider.GetDirectoryContents("wwwroot");
                        var images = root.First(x => x.Name == "Images");

                        var randomImageName = Guid.NewGuid() + Path.GetExtension(newBanner.Image4.FileName);


                        var path = Path.Combine(images.PhysicalPath, randomImageName);


                        using var stream = new FileStream(path, FileMode.Create);


                        newBanner.Image4.CopyTo(stream);



                        banner.ImagePath4 = randomImageName;

                    }

                    _context.Banners.Add(banner);
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








        [HttpGet]
        public IActionResult Update(int id)
        {


            var banner = _context.Banners.Find(id);

            ViewBag.LinkSelect = new SelectList(new List<CategorySelectList>() {

               new(){Data="Yatak Odası", Value="Yatak"},
                new(){Data="Bebe ve Genç Odaları", Value="Bebevegenc"},
                new(){Data="Tv Üniteleri", Value="Tv"},
                new(){Data="Yemek Odaları", Value="Yemek"},
                new(){Data="Mutfak Dolapları", Value="Mutfak"},

            }, "Value", "Data", banner.Link);

            return View(_mapper.Map<BannerViewModel>(banner));
        }


        [HttpPost]
        public IActionResult Update(BannerViewModel updateBanner)
        {

            ViewBag.LinkSelect = new SelectList(new List<CategorySelectList>() {

               new(){Data="Yatak Odası", Value="Yatak"},
                new(){Data="Bebe ve Genç Odaları", Value="Bebevegenc"},
                new(){Data="Tv Üniteleri", Value="Tv"},
                new(){Data="Yemek Odaları", Value="Yemek"},
                new(){Data="Mutfak Dolapları", Value="Mutfak"},

            }, "Value", "Data", updateBanner.Link);

            if (updateBanner.Image4 != null && updateBanner.Image4.Length > 0)
            {
                var root = _fileProvider.GetDirectoryContents("wwwroot");
                var images = root.First(x => x.Name == "Images");

                var randomImageName = Guid.NewGuid() + Path.GetExtension(updateBanner.Image4.FileName);


                var path = Path.Combine(images.PhysicalPath, randomImageName);


                using var stream = new FileStream(path, FileMode.Create);



                updateBanner.Image4.CopyTo(stream);



                updateBanner.ImagePath4 = randomImageName;

               
            


        }
          


            var banner = _mapper.Map<Banner>(updateBanner);
        _context.Banners.Update(banner);
            _context.SaveChanges();

            TempData["status"] = "Ürün Başarıyla Güncellendi.";

            return RedirectToAction("Index");


    }



    public IActionResult GetById(int BannerId)
    {
        var banner = _context.Banners.Find(BannerId);
        return View(_mapper.Map<BannerViewModel>(banner));


    }

}
}

