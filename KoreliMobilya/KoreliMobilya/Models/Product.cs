namespace KoreliMobilyaDeneme.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? ImagePath { get; set; }
        public string? ImagePath2 { get; set; }
        public string? ImagePath3 { get; set; }
        public string? ImagePath7 { get; set; }

        public string? Link { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public Category Category { get; set; }

    }
}
