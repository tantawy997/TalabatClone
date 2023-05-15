using Core.entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifiactions
{
    public class ProductWithCountSpecifiactions :BaseSpecifiactions<Product>
    {
        public ProductWithCountSpecifiactions(SpecifiactionsParams specifiactionsParams)
        : base(product =>
        (string.IsNullOrEmpty(specifiactionsParams.Search) || product.Name.ToLower().Contains(specifiactionsParams.Search))&&
        (!specifiactionsParams.BrandId.HasValue || product.ProductBrandId == specifiactionsParams.BrandId) &&
        (!specifiactionsParams.TypeId.HasValue || product.ProductTypeId == specifiactionsParams.TypeId)
        )
        {


        }

    }
}
