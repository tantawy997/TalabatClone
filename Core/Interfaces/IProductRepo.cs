using Core.entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductRepo
    {
        Task<Product> GetProductByIdAsync(int? id);

        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();

        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();

        Task<IReadOnlyList<Product>> GetProductsAsynce();

    }
}
