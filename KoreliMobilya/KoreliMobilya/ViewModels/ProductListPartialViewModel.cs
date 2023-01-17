namespace KoreliMobilyaDeneme.ViewModels
{
    public class ProductListPartialViewModel
    {
        public List<ProductPartialViewModel> Products { get; set; }
    }

    public class ProductPartialViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? ImagePath { get; set; }
        public string? ImagePath2 { get; set; }
        public string? ImagePath3 { get; set; }
        public string? Link { get; set; }
        public int CategoryId { get; set; }

    }
}
