using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using pruaccount.api.AppSettings;
using pruaccount.api.Extensions;
using pruaccount.api.Middleware;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace pruaccount.api
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
            services.AddCors();

            TokenConfigSetting tokenConfig = Configuration.GetSection("Token").Get<TokenConfigSetting>();

            services.AddAntiforgery(options => {
                //options.Cookie.Name = "Antiforgery";
                options.HeaderName = tokenConfig.AntiforgeryTokenCookieHeader;
                options.SuppressXFrameOptionsHeader = true;
            });
            services.AddHttpContextAccessor();

            // Configure Compression level
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
            });

            services.Configure<DBInfoConfigSetting>(Configuration.GetSection("DBInfo"));
            services.Configure<TokenConfigSetting>(Configuration.GetSection("Token"));

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IAntiforgery antiforgery, ILogger<Startup> logger)
        {
            logger.LogInformation("Configure called");

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

            app.UseResponseCompression();

            app.UseAntiforgeryToken();
            app.ValidateAntiforgeryTokens();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });


            app.UseRouting();
            app.UseCors(x => x
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 //.WithOrigins("https://*.prudentserviceslocal.com")
                 //.SetIsOriginAllowedToAllowWildcardSubdomains()
                 .SetIsOriginAllowed(origin => true) // allow any origin
                 .AllowCredentials()); // allow credentials

            app.Use((context, next) =>
            {
                context.Response.Headers.Add("Server", string.Empty);
                return next();
            });


            app.UseAuthentication();
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
