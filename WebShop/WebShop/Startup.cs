
using AutoMapper;
using DAL.Dtos;
using DAL.Models;
using DAL.ServiceInterfaces;
using DAL.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<WebShopSampleContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("conn")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<WebShopSampleContext>();

            
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);


            services.AddScoped<ICategorySQLRepository, CategorySQLRepository>();
            services.AddScoped<ISubCategorySQLRepository, SubCategorySQLRepository>();
            services.AddScoped<IItemBrandSQLRepository, ItemBrandSQLRepository>();
            services.AddScoped<IItemSQLRepository, ItemSQLRepository>();
            services.AddScoped<IDiscountSQLRepository, DiscountSQLRepository>();
            services.AddScoped<IItemDiscountsSQLRepository, ItemDiscountSQLRepository>();
            services.AddScoped<IPayMethodSQLRepository, PayMethodSQLRepository>();
            

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
