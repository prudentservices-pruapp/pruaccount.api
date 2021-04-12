// <copyright file="Startup.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api
{
    using System.IO.Compression;
    using Microsoft.AspNetCore.Antiforgery;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.HttpOverrides;
    using Microsoft.AspNetCore.ResponseCompression;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Pruaccount.Api.AppSettings;
    using Pruaccount.Api.DataAccess.Core;
    using Pruaccount.Api.Domain.Auth;
    using Pruaccount.Api.Extensions;
    using Pruaccount.Api.HttpClients.AuthValidationClient;
    using PruAuth.Api.Domain.Auth;

    /// <summary>
    /// Startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">IConfiguration.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">IServiceCollection.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            TokenConfigSetting tokenConfig = this.Configuration.GetSection("Token").Get<TokenConfigSetting>();

            services.AddAntiforgery(options =>
            {
                // options.Cookie.Name = "Antiforgery";
                options.HeaderName = tokenConfig.AntiforgeryTokenCookieHeader;
                options.SuppressXFrameOptionsHeader = true;
            });

            // Token
            services.AddTransient<ITokenUtils, TokenUtils>();
            services.AddHttpClient<IValidateUserTokenClient, ValidateUserTokenClient>();

            services.AddHttpContextAccessor();

            // Configure Compression level
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
            });

            services.Configure<DBInfoConfigSetting>(this.Configuration.GetSection("DBInfo"));
            services.Configure<TokenConfigSetting>(this.Configuration.GetSection("Token"));

            Dapper.SqlMapper.AddTypeHandler(new DapperDateTimeUTC());
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddControllersWithViews();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">IApplicationBuilder.</param>
        /// <param name="env">IWebHostEnvironment.</param>
        /// <param name="antiforgery">IAntiforgery.</param>
        /// <param name="logger">logger.</param>
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

            // app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
            });

            app.UseRouting();
            app.UseCors(x => x
                 .AllowAnyMethod()
                 .AllowAnyHeader()

                 // .WithOrigins("https://*.prudentserviceslocal.com")
                 // .SetIsOriginAllowedToAllowWildcardSubdomains()
                 .SetIsOriginAllowed(origin => true) // allow any origin
                 .AllowCredentials()); // allow credentials

            app.Use((context, next) =>
            {
                context.Response.Headers.Add("Server", string.Empty);
                return next();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            // Section for Custom - https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-3.0#order
            app.UseAccessToken();

            app.UseAntiforgeryToken();
            app.ValidateAntiforgeryTokens();

            app.UseResponseCompression();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}