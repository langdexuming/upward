using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reggie.Blog.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Reggie.Blog.Models;

namespace Reggie.Blog
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

            if (Configuration.GetValue<bool>("UseMSSQLServer"))
            {
                services.AddDbContext<BlogContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
                services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            }
            else
            {
                services.AddDbContext<BlogContext>(opt => opt.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));
                services.AddDbContext<ApplicationDbContext>(opt => opt.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));
            }

            services.AddMvc();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //// Add application services.
            //services.AddTransient<IEmailSender, AuthMessageSender>();
            //services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
