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
using System.Net.WebSockets;
using Microsoft.AspNetCore.WebSockets;
using System.Threading;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace Reggie.Upward.WebApi
{
    public class Startup
    {
        private const string HostAddress= "http://localhost:5001";

        private ILogger _logger;
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
                services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
                services.AddDbContext<Areas.Car.Data.CarContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
                services.AddDbContext<Areas.PlatformAccount.Data.PlatformAccountContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(opt => opt.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));
                services.AddDbContext<Areas.Car.Data.CarContext>(opt => opt.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));
                services.AddDbContext<Areas.PlatformAccount.Data.PlatformAccountContext>(opt => opt.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));
            }

            services.AddMvc();

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

            services.AddMvcCore().AddAuthorization().AddJsonFormatters();
            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = HostAddress;
                    options.RequireHttpsMetadata = false;

                    options.ApiName = Config.ApiName;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //
            }

            _logger = loggerFactory.CreateLogger<Startup>();

            app.UseIdentityServer();

            //支持跨域
            app.UseCors(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.WithOrigins(HostAddress);
            });

            //支持Websocket通信
            app.UseWebSockets();

            app.Use(async (context, next) =>
            {
                _logger.LogDebug(1,null,"Request Path:"+context.Request.Path);
                if (context.WebSockets.IsWebSocketRequest)
                {
                    var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    await Echo(context, webSocket, loggerFactory.CreateLogger("Echo"));
                }
                else
                {
                    await next();
                }
            });

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

        private async Task Echo(HttpContext context, WebSocket webSocket, ILogger logger)
        {
            var buffer = new byte[1024 * 4];
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            LogFrame(logger, result, buffer);
            while (!result.CloseStatus.HasValue)
            {
                // If the client send "ServerClose", then they want a server-originated close to occur
                string content = "<<binary>>";
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    content = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    if (content.Equals("ServerClose"))
                    {
                        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing from Server", CancellationToken.None);
                        logger.LogDebug($"Sent Frame Close: {WebSocketCloseStatus.NormalClosure} Closing from Server");
                        return;
                    }
                    else if (content.Equals("ServerAbort"))
                    {
                        context.Abort();
                    }
                }

                await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);
                logger.LogDebug($"Sent Frame {result.MessageType}: Len={result.Count}, Fin={result.EndOfMessage}: {content}");

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                LogFrame(logger, result, buffer);
            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }

        private void LogFrame(ILogger logger, WebSocketReceiveResult frame, byte[] buffer)
        {
            var close = frame.CloseStatus != null;
            string message;
            if (close)
            {
                message = $"Close: {frame.CloseStatus.Value} {frame.CloseStatusDescription}";
            }
            else
            {
                string content = "<<binary>>";
                if (frame.MessageType == WebSocketMessageType.Text)
                {
                    content = Encoding.UTF8.GetString(buffer, 0, frame.Count);
                }
                message = $"{frame.MessageType}: Len={frame.Count}, Fin={frame.EndOfMessage}: {content}";
            }
            logger.LogDebug("Received Frame " + message);
        }

    }
}
