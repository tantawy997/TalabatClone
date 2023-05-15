using AutoMapper;
using Core.entites;
using Core.entites.Identity;
using WepAPIAssignment.Dtos;

namespace WepAPIAssignment.Helpers
{
    public class ProfilesToMap : Profile
    {
        public ProfilesToMap()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(product => product.BrandName, brand => brand.MapFrom(src => src.ProductBrand.Name))
                .ForMember(Product => Product.TypeName, type => type.MapFrom(src => src.ProductType.Name))
                //ReverseMap();
                .ForMember(Product => Product.PictureUrl, type => type.MapFrom<ProductResolver>());

            CreateMap<BasketItems, BasketItemsDTO>().ReverseMap();
            CreateMap<CustomerBasket, CustomerBasketDTO>().ReverseMap();

            CreateMap<Address, AddressDTO>().ReverseMap();

        }
    }
}
