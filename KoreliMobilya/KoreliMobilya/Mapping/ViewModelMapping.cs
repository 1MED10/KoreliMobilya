using AutoMapper;
using KoreliMobilyaDeneme.Models;
using KoreliMobilyaDeneme.ViewModels;

namespace KoreliMobilyaDeneme.Mapping
{
    public class ViewModelMapping:Profile
    {
        public ViewModelMapping()
        {
            //Producttan Diğerine çevirir, ReverseMAp ise tam tersi olması gerekiyorsa diye yazdık.
            CreateMap<Product, ProductViewModel>().ReverseMap();

            CreateMap<Banner, BannerViewModel>().ReverseMap();
            CreateMap<Category, CategoryViewModel>().ReverseMap();

            CreateMap<Product, ProductUpdateViewModel>().ReverseMap();

           ;
        }

    }
}
