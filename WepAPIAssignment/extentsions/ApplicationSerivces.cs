using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WepAPIAssignment.Helpers;
using WepAPIAssignment.ResponseModule;

namespace WepAPIAssignment.extentsions
{
    public static class ApplicationSerivces
    {

        public static IServiceCollection AddApplicationSerivce(this IServiceCollection Services)
        {

            Services.AddScoped(typeof(IGenricRepo<>), typeof(GenricRepo<>));
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<IProductRepo, ProductRepo>();
            Services.AddAutoMapper(typeof(ProfilesToMap));
            Services.AddScoped<IBasketRepo, BasketRepo>();
            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionconext =>
                {
                    var Erros = actionconext.ModelState.Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(e => e.Value.Errors)
                    .Select(m => m.ErrorMessage)
                    .ToArray();

                    var errorResponse = new ApiValdationErrors
                    {
                        errors = Erros
                    };

                    return new BadRequestObjectResult(errorResponse);
                };

            });

            return Services;
        }
    }
}
