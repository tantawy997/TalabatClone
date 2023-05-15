using Core.entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifiactions
{
    public class ProductWithBrandAndTypeSpecifiactions : BaseSpecifiactions<Product>
    {
        public ProductWithBrandAndTypeSpecifiactions(SpecifiactionsParams specifiactionsParams) 
            : base(product =>
            (string.IsNullOrEmpty(specifiactionsParams.Search) || product.Name.ToLower().Contains(specifiactionsParams.Search))&&
            (!specifiactionsParams.BrandId.HasValue|| product.ProductBrandId == specifiactionsParams.BrandId)&&
            (!specifiactionsParams.TypeId.HasValue || product.ProductTypeId == specifiactionsParams.TypeId)      
            )
        {

            AddIncludes(Product => Product.ProductType);
            AddIncludes(Product => Product.ProductBrand);
            AddOrderBy(product => product.Name);
            AddPagination(specifiactionsParams.pageSize * (specifiactionsParams.pageIndex - 1), 
                specifiactionsParams.pageSize);

            if (!string.IsNullOrEmpty(specifiactionsParams.Sort))
            {
                switch (specifiactionsParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(prod => prod.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(prod => prod.Price);
                        break;
                    default:
                        AddOrderBy(prod => prod.Name);
                        break;

                }
            }
        }

        public ProductWithBrandAndTypeSpecifiactions(int id)
            :base(product => product.Id == id)
        {
            AddIncludes(p => p.ProductType);
            AddIncludes(p => p.ProductBrand);

        }
    }
    
}
