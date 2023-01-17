using AutoMapper;
using KoreliMobilyaDeneme.Filters;
using KoreliMobilyaDeneme.Models;
using KoreliMobilyaDeneme.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace KoreliMobilyaDeneme.Controllers
{
    public class ProductsController : Controller
    {
        private  AppDbContext _context;
        private readonly ProductRepository _productRepository;
        private readonly IFileProvider _fileProvider;
        private readonly IMapper _mapper;


        public ProductsController(AppDbContext context, IMapper mapper, IFileProvider fileProvider)
        {
            _productRepository = new ProductRepository();
            _context = context;
            _mapper = mapper;          
            _fileProvider = fileProvider;
        }





        public IActionResult Index()
        {

            List<ProductViewModel> products = _context.Products.Include(x => x.Category).Select(x => new ProductViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                CategoryName = x.Category.Name,
                ImagePath = x.ImagePath,
                ImagePath2 = x.ImagePath2,
                ImagePath3 = x.ImagePath3,
                ImagePath7 = x.ImagePath7,

            }).ToList();
            return View(products);
   }








        [ServiceFilter(typeof(NotFoundFilter))]
        public IActionResult Remove(int id)
        {
            var product = _context.Products.Find(id);
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }










        [HttpGet]
        public IActionResult Add()

        {

            ViewBag.LinkSelect = new SelectList(new List<CategorySelectList>() {

                new(){Data="Yatak Odaları", Value="Yatak"},
                new(){Data="Bebe ve Genç Odaları", Value="Bebevegenc"},
                new(){Data="Tv Üniteleri", Value="Tv"},
                new(){Data="Yemek Odaları", Value="Yemek"},
                new(){Data="Mutfak Dolapları", Value="Mutfak"},

            }, "Value", "Data");


            var categories = _context.Category.ToList();
            //Id hangisini alacağını name ise hangisini kullanıcya göstereceğini seçmemize yarıyor
            ViewBag.categorySelect = new SelectList(categories, "Id", "Name");


            return View();
        }


        //Burda ise kullanıcıdan bilgiyi aldık ve required alanı girildiyse kaydet diyoruz eğer değilse hatayı göster diyoruz IsValid Girilmişmi Girilmemişmi kontrolunu sağlıyor.
        [HttpPost]
        public IActionResult Add(ProductViewModel newProduct)
        {


            IActionResult result = null;


            if (ModelState.IsValid)
            {

                try
                {

                    var product = _mapper.Map<Product>(newProduct);
                    if (newProduct.Image != null && newProduct.Image.Length > 0)
                    {
                        var root = _fileProvider.GetDirectoryContents("wwwroot");
                        var images = root.First(x => x.Name == "Images");

                        var randomImageName = Guid.NewGuid() + Path.GetExtension(newProduct.Image.FileName);
                        var randomImageName2 = Guid.NewGuid() + Path.GetExtension(newProduct.Image2.FileName);
                        var randomImageName3 = Guid.NewGuid() + Path.GetExtension(newProduct.Image3.FileName);
                        var randomImageName7 = Guid.NewGuid() + Path.GetExtension(newProduct.Image7.FileName);

                        var path = Path.Combine(images.PhysicalPath, randomImageName);
                        var path2 = Path.Combine(images.PhysicalPath, randomImageName2);
                        var path3 = Path.Combine(images.PhysicalPath, randomImageName3);
                        var path7 = Path.Combine(images.PhysicalPath, randomImageName7);

                        using var stream = new FileStream(path, FileMode.Create);
                        using var stream2 = new FileStream(path2, FileMode.Create);
                        using var stream3 = new FileStream(path3, FileMode.Create);
                        using var stream7 = new FileStream(path7, FileMode.Create);

                        newProduct.Image.CopyTo(stream);
                        newProduct.Image2.CopyTo(stream2);
                        newProduct.Image3.CopyTo(stream3);
                        newProduct.Image7.CopyTo(stream7);


                        product.ImagePath = randomImageName;
                        product.ImagePath2 = randomImageName2;
                        product.ImagePath3 = randomImageName3;
                        product.ImagePath7 = randomImageName7;
                    }
                    _context.Products.Add(product);
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

            var categories = _context.Category.ToList();
            ViewBag.categorySelect = new SelectList(categories, "Id", "Name");

            return result;
        }







        [ServiceFilter(typeof(NotFoundFilter))]
        [HttpGet]
        public IActionResult Update(int id)
        {

            var product = _context.Products.Find(id);

            ViewBag.LinkSelect = new SelectList(new List<CategorySelectList>() {

               new(){Data="Yatak Odaları", Value="Yatak"},
                new(){Data="Bebe ve Genç Odaları", Value="Bebevegenc"},
                new(){Data="Tv Üniteleri", Value="Tv"},
                new(){Data="Yemek Odaları", Value="Yemek"},
                new(){Data="Mutfak Dolapları", Value="Mutfak"},

            }, "Value", "Data", product.Link);


          
            ViewBag.LinkSelect = new SelectList(new List<CategorySelectList>() {

               new(){Data="Yatak Odaları", Value="Yatak"},
                new(){Data="Bebe ve Genç Odaları", Value="Bebevegenc"},
                new(){Data="Tv Üniteleri", Value="Tv"},
                new(){Data="Yemek Odaları", Value="Yemek"},
                new(){Data="Mutfak Dolapları", Value="Mutfak"},

            }, "Value", "Data", product.Link);


            var categories = _context.Category.ToList();
            ViewBag.categorySelect = new SelectList(categories, "Id", "Name", product.CategoryId);



            return View(_mapper.Map<ProductUpdateViewModel>(product));
        }



        [HttpPost]
        public IActionResult Update(ProductUpdateViewModel updateProduct)
        {
            ViewBag.LinkSelect = new SelectList(new List<CategorySelectList>() {

               new(){Data="Yatak Odaları", Value="Yatak"},
                new(){Data="Bebe ve Genç Odaları", Value="Bebevegenc"},
                new(){Data="Tv Üniteleri", Value="Tv"},
                new(){Data="Yemek Odaları", Value="Yemek"},
                new(){Data="Mutfak Dolapları", Value="Mutfak"},

            }, "Value", "Data", updateProduct.Link);


            if (updateProduct.Image != null && updateProduct.Image.Length > 0)
            {
                var root = _fileProvider.GetDirectoryContents("wwwroot");
                var images = root.First(x => x.Name == "Images");

                var randomImageName = Guid.NewGuid() + Path.GetExtension(updateProduct.Image.FileName);
                var randomImageName2 = Guid.NewGuid() + Path.GetExtension(updateProduct.Image2.FileName);
                var randomImageName3 = Guid.NewGuid() + Path.GetExtension(updateProduct.Image3.FileName);
                var randomImageName7 = Guid.NewGuid() + Path.GetExtension(updateProduct.Image7.FileName);

                var path = Path.Combine(images.PhysicalPath, randomImageName);
                var path2 = Path.Combine(images.PhysicalPath, randomImageName2);
                var path3 = Path.Combine(images.PhysicalPath, randomImageName3);
                var path7 = Path.Combine(images.PhysicalPath, randomImageName7);

                using var stream = new FileStream(path, FileMode.Create);
                using var stream2 = new FileStream(path2, FileMode.Create);
                using var stream3 = new FileStream(path3, FileMode.Create);
                using var stream7 = new FileStream(path7, FileMode.Create);


                updateProduct.Image.CopyTo(stream);
                updateProduct.Image2.CopyTo(stream2);
                updateProduct.Image3.CopyTo(stream3);
                updateProduct.Image7.CopyTo(stream7);


                updateProduct.ImagePath = randomImageName;
                updateProduct.ImagePath2 = randomImageName2;
                updateProduct.ImagePath3 = randomImageName3;
                updateProduct.ImagePath7 = randomImageName7;
            }

            var product = _mapper.Map<Product>(updateProduct);


            _context.Products.Update(product);
            _context.SaveChanges();

            TempData["status"] = "Ürün Başarıyla Güncellendi.";

            return RedirectToAction("Index");
        }









        [AcceptVerbs("GET", "POST")]
        public IActionResult HasProductName(string Name)
        {
            var anyProduct = _context.Products.Any(x => x.Name.ToLower() == Name.ToLower());

            if (anyProduct)
            {
                return Json("Kaydetmeye Çalıştığınız Mdeol ismi veritabanında          bulunmaktadır.");
            }
            else
            {
                return Json(true);
            }
        }










        [ServiceFilter(typeof(NotFoundFilter))]
        [Route("modeller/model/{productid}", Name = "product")]
        public IActionResult GetById(int ProductId)
        {
         

            var product = _context.Products.Find(ProductId);
            return View(_mapper.Map<ProductViewModel>(product));


        }



    }
}
