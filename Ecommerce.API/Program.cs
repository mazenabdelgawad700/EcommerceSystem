using Ecommerce.Core;
using Ecommerce.Core.MiddelWare;
using Ecommerce.Infrastructure;
using Ecommerce.Infrastructure.Context;
using Ecommerce.Service.Implementation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddAuthorizationBuilder()
                    .AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"))
                    .AddPolicy("SellerOnly", policy => policy.RequireRole("Seller"));


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Register To DI
            builder.Services
                .AddInfrastructureDependencies()
                .AddServiceDependencies()
                .AddCoreDependencies()
                .AddServiceRegisteration(builder.Configuration);

            #region AllowCors
            string CORS = "_cors";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: CORS, policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            });
            #endregion

            builder.Services.AddHostedService<CartCleanupService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseHttpsRedirection();

            app.UseCors(CORS);

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                string[] roleNames = ["Admin", "Buyer", "Seller"];
                if (!roleManager.Roles.Any())
                {
                    foreach (var roleName in roleNames)
                    {
                        if (!await roleManager.RoleExistsAsync(roleName))
                        {
                            await roleManager.CreateAsync(new IdentityRole(roleName));
                        }
                    }
                }
                scope.Dispose();
            }

            app.Run();
        }
    }
}
