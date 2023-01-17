using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace KoreliMobilyaDeneme.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Remote(action: "HasProductName", controller: "Products")]
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Model İsmi Boş Olamaz")]
        public string Name { get; set; }


        public IFormFile? Image { get; set; }
        public IFormFile? Image2 { get; set; }
        public IFormFile? Image3 { get; set; }
        public IFormFile? Image7 { get; set; }

        [ValidateNever]
        public string? ImagePath { get; set; }

        [ValidateNever]
        public string? ImagePath2 { get; set; }

        [ValidateNever]
        public string? ImagePath3 { get; set; }

        [ValidateNever]
        public string? ImagePath7 { get; set; }

        public string? Link { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Kategori Seçimi Boş Olamaz")]
        public int CategoryId { get; set; }


        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Kategori Seçimi  Boş Olamaz")]
        public string? CategoryName { get; set; }


    }
}
