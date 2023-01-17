namespace KoreliMobilyaDeneme.Models
{
    public class BannerRepository
    {
        private readonly List<Banner> _banners;

        public List<Banner> GetAll() => _banners;

        public void add(Banner newBanner) => _banners.Add(newBanner);

        public void Remove(int id)
        {
            var hasBanner = _banners.FirstOrDefault(x => x.Id == id);

            if (hasBanner == null)
            {
                throw new Exception($"Bu id({id})'ye sahip ürün bulunmamaktadır.");
            }
            _banners.Remove(hasBanner);
        }

        public void Update(Banner updateBanner)
        {
            var hasBanner = _banners.FirstOrDefault(x => x.Id == updateBanner.Id);

            if (hasBanner == null)
            {
                throw new Exception($"Bu id({updateBanner.Id})'ye sahip ürün bulunmamaktadır.");
            }

            hasBanner.Name = updateBanner.Name;

            var index = _banners.FindIndex(x => x.Id == updateBanner.Id);

            _banners[index] = hasBanner;
        }

    }
}
