using Core.entites.Identity;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Identity;
using Infrastructure.Serivces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using WepAPIAssignment.extentsions;
using WepAPIAssignment.Helpers;
using WepAPIAssignment.MiddleWirres;

namespace WepAPIAssignment
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


            //builder.Services.AddScoped(typeof(IGenricRepo<>),typeof(GenricRepo<>));
            //builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            //builder.Services.AddScoped<IProductRepo, ProductRepo>();

            builder.Services.AddDbContext<StoreDbContext>(o =>
            {
            o.UseSqlServer(builder.Configuration.GetConnectionString("DefualtConnection"));

            });

            builder.Services.AddDbContext<AppIdentityDbContext>(o =>
            {
                o.UseSqlServer(builder.Configuration.GetConnectionString("identityConnection"));
            });
            builder.Services.AddApplicationSerivce();
            builder.Services.AddIdentityConfigrution(builder.Configuration);
            builder.Services.AddSwaggerService();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            //builder.Services.AddIdentityCore<AppUser>();
            builder.Services.AddScoped<IResponseCache, ServiceCache>();
            builder.Services.AddCors(option =>
            {
                option.AddPolicy("CorsPolicy", policy =>
                policy.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().SetIsOriginAllowed(origin => true));
            });

            builder.Services.AddSingleton<IConnectionMultiplexer>(config =>
            {
                var configurtions = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"),
                    true);
                return ConnectionMultiplexer.Connect(configurtions);
            });
            //builder.Services.AddAutoMapper(typeof(ProfilesToMap));

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                var logger = service.GetRequiredService<ILoggerFactory>();
                try
                {
                    var context = service.GetRequiredService<StoreDbContext>();
                    await context.Database.MigrateAsync();
                    await StoreDBContextSeed.SeedDataAsync(context, logger);

                    var userManager = service.GetRequiredService<UserManager<AppUser>>();
                    var Identitycontext = service.GetRequiredService<AppIdentityDbContext>();

                    await context.Database.MigrateAsync();
                    await AppIdentitySeeding.SeedUserAsync(userManager);

                }
                catch (Exception ex)
                {
                    
                    var log = logger.CreateLogger<Program>();
                    log.LogError(ex, "an error occured while seeding the data");
                }
            }
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c=>c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api demo v1"));
            }
            //app.UseMiddleware<MiddleWirreException>();
            //app.UseMiddleware<ExpectionMiddlwirres>();
            app.UseCors("CorsPolicy");
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            

            app.MapControllers();

            app.Run();
        }
    }
}