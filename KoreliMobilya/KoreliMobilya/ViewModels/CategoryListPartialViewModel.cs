namespace KoreliMobilyaDeneme.ViewModels
{
    public class CategoryListPartialViewModel
    {
        public List<CategoryPartialViewModel> Categories { get; internal set; }

        public class CategoryPartialViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }

            public string ImagePath5 { get; set; }
        }
    }
}
