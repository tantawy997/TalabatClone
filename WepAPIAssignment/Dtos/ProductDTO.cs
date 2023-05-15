using Core.entites;

namespace WepAPIAssignment.Dtos
{
    public class ProductDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public string Description { get; set; } = string.Empty;

        public string PictureUrl { get; set; } = "";
        
        public decimal Price { get; set; }

        public string BrandName { get; set; }

        public string TypeName { get; set; }

    }
}
