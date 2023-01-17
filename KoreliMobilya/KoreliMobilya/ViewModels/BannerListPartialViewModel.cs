namespace KoreliMobilyaDeneme.ViewModels
{
    public class BannerListPartialViewModel
    {
       
           
        public List<BannerPartialViewModel> Banners { get; internal set; }

        public class BannerPartialViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public string ImagePath4 { get; set; }
            public string Link { get; set; }
        }

    }
}
