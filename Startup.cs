using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restarter.Data;
using Restarter.Services;

namespace Restarter
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RestarterDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<RestartTrigger>();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseMiddleware<EnforceHttpsMiddleware>();
                app.UseHsts();
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }

    public class EnforceHttpsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public EnforceHttpsMiddleware(RequestDelegate next,
            IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            var hsts = Convert.ToBoolean(_configuration["HSTS"]);
            if (hsts && !context.Response.Headers.ContainsKey("Strict-Transport-Security"))
            {
                context.Response.Headers.Add("Strict-Transport-Security", "max-age=15552001; includeSubDomains; preload");
            }
            if (!context.Request.IsHttps)
            {
                await HandleNonHttpsRequest(context);
            }
            else
            {
                await _next.Invoke(context);
            }
        }
        protected virtual async Task HandleNonHttpsRequest(HttpContext context)
        {
            if (!string.Equals(context.Request.Method, "GET", StringComparison.OrdinalIgnoreCase))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
            }
            else
            {
                string newUrl = string.Empty;
                await Task.Run(() =>
                {
                    var optionsAccessor = context.RequestServices.GetRequiredService<IOptions<MvcOptions>>();
                    var request = context.Request;
                    var host = request.Host;
                    if (optionsAccessor.Value.SslPort.HasValue && optionsAccessor.Value.SslPort > 0)
                    {
                        host = new HostString(host.Host, optionsAccessor.Value.SslPort.Value);
                    }
                    else
                    {
                        host = new HostString(host.Host);
                    }
                    newUrl = string.Concat(
                        "https://",
                        host.ToUriComponent(),
                        request.PathBase.ToUriComponent(),
                        request.Path.ToUriComponent(),
                        request.QueryString.ToUriComponent());
                });
                context.Response.Redirect(newUrl, permanent: true);
            }
        }
    }
