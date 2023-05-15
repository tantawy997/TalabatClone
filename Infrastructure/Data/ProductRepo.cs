using Core.entites;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ProductRepo : IProductRepo
    {
        private readonly StoreDbContext context;

        public ProductRepo(StoreDbContext _context) 
        {
            context = _context;
        }
        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
           return await context.ProductBrands.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int? id)
        {
            return await context.Products.FirstOrDefaultAsync(product=> product.Id == id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsynce()
        {
            return await context.Products
                .Include(a=>a.ProductBrand)
                .Include(a=>a.ProductType).ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await context.ProductTypes.ToListAsync();
        }
    }
}
