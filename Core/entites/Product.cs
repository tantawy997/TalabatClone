using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.entites
{
    public class Product :BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public string Description { get; set; } = string.Empty;

        public string PictureUrl { get; set; } = "";

        public decimal Price { get; set; }

        //[ForeignKey("ProductType")]

        //[ForeignKey("ProductBrand")]
        public int ProductBrandId { get; set; }

        public ProductBrand ProductBrand { get; set; } 

        public int ProductTypeId { get; set; }


        public ProductType ProductType { get; set; }





    }
}
