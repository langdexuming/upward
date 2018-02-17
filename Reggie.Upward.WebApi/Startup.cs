using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Reggie.Upward.WebApi.Data;

namespace Reggie.Upward.WebApi
{
    public class Startup
    {
        private const string HostAddress= "http://localhost:5001";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
#if true
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<Areas.Car.Data.CarContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
#else
            services.AddDbContext<CarContext>(opt => opt.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));
#endif
            services.AddMvc();

            // _logger = services.BuildServiceProvider().GetService<ILogger<Startup>>();

            // _logger.LogInformation(LoggingEvents.ConfigureServices, "ConfigureServices", "");

            //支持跨域
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSameDomain",
                builder => builder.WithOrigins(HostAddress).AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().AllowCredentials());
            });

            // configure identity server with in-memory stores, keys, clients and resources
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients());

            services.AddMvcCore()
           .AddAuthorization()
           .AddJsonFormatters();

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = HostAddress;
                    options.RequireHttpsMetadata = false;

                    options.ApiName = Config.ApiName;
                });
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
                //
            }

            app.UseIdentityServer();

            ////支持跨域
            //app.UseCors(builder =>
            //{
            //    builder.AllowAnyHeader();
            //    builder.AllowAnyMethod();
            //    builder.WithOrigins(HostAddress);
            //});

            //支持Area
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "area",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
