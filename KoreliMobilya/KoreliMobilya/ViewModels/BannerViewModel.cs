using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace KoreliMobilyaDeneme.ViewModels
{
    public class BannerViewModel
    {
        public int Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Model İsmi Boş Olamaz")]
        public string Name { get; set; }

        public IFormFile? Image4 { get; set; }

        [ValidateNever]
        public string? ImagePath4 { get; set; }

        public string? Link { get; set; }


    }
}
