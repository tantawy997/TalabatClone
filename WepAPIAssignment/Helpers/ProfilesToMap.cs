using AutoMapper;
using Core.entites;
using Core.entites.Identity;
using Core.entites.OrderAggragate;
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

            CreateMap<ShippingAddress, ShippingAddressDTO>().ReverseMap();

            //CreateMap<Order, OrderDTO>().ReverseMap();

            CreateMap<Order, OrderDetailsDTO>()
                .ForMember(dest => dest.delivaryMethod, option => option.MapFrom(src => src.delivaryMethod.ShortName))
                .ForMember(dest => dest.ShippingPrice, option => option.MapFrom(src => src.delivaryMethod.Price));

            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(dest => dest.ProductId, option => option.MapFrom(src => src.ItemOrdered.ProductItemId))
                .ForMember(dest => dest.ProductName, option => option.MapFrom(src => src.ItemOrdered.ProductName))
                .ForMember(dest => dest.PictureUrl, option => option.MapFrom(src => src.ItemOrdered.PictureUrl))
                .ForMember(dest => dest.PictureUrl, option => option.MapFrom<OrderItemUrlResolver>());




        }
    }
}
