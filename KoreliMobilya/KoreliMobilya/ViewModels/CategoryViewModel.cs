using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace KoreliMobilyaDeneme.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IFormFile? Image5 { get; set; }

        [ValidateNever]
        public string? ImagePath5 { get; set; }
    }
}
