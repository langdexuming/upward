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
using Reggie.Upward.WebApi.Models;

namespace Reggie.Upward.WebApi
{
    public class Startup
    {
        //private ILogger _logger;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
#if true
            services.AddDbContext<CarContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
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
                builder => builder.WithOrigins("http://localhost:8081").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().AllowCredentials());
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

            app.UseMvc();

            // //支持跨域
            // app.UseCors(builder =>
            // {
            //     builder.AllowAnyHeader();
            //     builder.AllowAnyMethod();
            //     builder.WithOrigins("http://localhost:8081");
            // });
        }
    }
}
