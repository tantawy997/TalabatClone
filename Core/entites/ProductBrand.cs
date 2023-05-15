using System.ComponentModel.DataAnnotations.Schema;

namespace Core.entites
{
    public class ProductBrand :BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Name { get; set; } = "";


    }
}