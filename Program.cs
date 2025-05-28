using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using ProductAdminPanel.DAL.DbContex;
using ProductAdminPanel.Services;
using ProductAdminPanel.Services.Implementations;
using ProductAdminPanel.Services.Interfaces;

namespace ProductAdminPanel
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add MudBlazor services
            builder.Services.AddMudServices();

            // Add Blazor and Razor Pages
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            // Register EF Core DbContext
            builder.Services.AddDbContext<ProductAdminDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ProductAdminDb")));



            //Register Application Services
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ISupplierService, SupplierService>();

            var app = builder.Build();

            // Configure HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}
