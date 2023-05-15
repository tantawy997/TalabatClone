using System.ComponentModel.DataAnnotations;

namespace WepAPIAssignment.Dtos
{
    public class BasketItemsDTO
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        [Range(0.1,double.MaxValue,ErrorMessage = "price must be at grater than 0")]
        public int Price { get; set; }
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "quantity must be at least 1")]

        public int quntity { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        public string brand { get; set; }
        [Required]
        public string type { get; set; }
    }
}