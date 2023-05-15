using AutoMapper;
using Core.entites;
using WepAPIAssignment.Dtos;

namespace WepAPIAssignment.Helpers
{
    public class ProductResolver : IValueResolver<Product, ProductDTO, string>
    {
        private readonly IConfiguration configuration;

        public ProductResolver(IConfiguration _configuration) 
        {
            configuration = _configuration;
        }
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
               return configuration["ApiUrl"] + source.PictureUrl;
            }
            else
            {
                return configuration["ApiUrl"];
            }
        }
    }
}
