using Core.entites;
using Core.entites.OrderAggragate;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class StoreDBContextSeed
    {
        public static async Task SeedDataAsync(StoreDbContext context, ILoggerFactory loggerFactory)
        {
			try
			{
				if(context.ProductBrands != null && !context.ProductBrands.Any())
				{
					var BrandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
					var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
                    
                    context.ChangeTracker.Clear();

                    foreach (var brand in brands)
					{
					   await context.AddAsync(brand);
					}
                    await context.SaveChangesAsync();


                }

                if (context.ProductTypes != null && !context.ProductTypes.Any())
                {
                    var TypesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                    var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);

                    foreach (var type in Types)
                    {
                       await context.AddAsync(type);
                    }
                    await context.SaveChangesAsync();

                }

                if (context.Products != null && !context.Products.Any())
                {
                    var ProductsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

                    foreach (var Product in products)
                    {
                        await context.AddAsync(Product);
                    }
                    await context.SaveChangesAsync();

                }
                if (context.delivaryMethods != null && !context.delivaryMethods.Any())
                {
                    var delivaryMethods = File.ReadAllText("../Infrastructure/Data/SeedData/delivery.json");
                    var delivaryMethod = JsonSerializer.Deserialize<List<DelivaryMethod>>(delivaryMethods);

                    foreach (var Method in delivaryMethod)
                    {
                        await context.AddAsync(Method);
                    }
                    await context.SaveChangesAsync();

                }
            }
			catch (Exception ex)
			{

				var logger= loggerFactory.CreateLogger<StoreDBContextSeed>();

                logger.LogError(ex.Message);
			}
        } 
    }
}
