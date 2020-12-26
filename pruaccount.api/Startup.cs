using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using pruaccount.api.Middleware;
using System;
using System.Collections.Generic;
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

            services.AddAntiforgery(options => {
                //options.Cookie.Name = "Antiforgery";
                options.HeaderName = "X-XSRF-TOKEN-ACC";
                options.SuppressXFrameOptionsHeader = true;
            });

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IAntiforgery antiforgery, ILogger<Startup> logger)
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

            logger.LogInformation("Configure called");

            app.Use(next => context =>
            {
                string path = context.Request.Path.Value;

                //if (path.StartsWith("/api/User/TestToken", StringComparison.OrdinalIgnoreCase))
                if (!path.StartsWith("/api", StringComparison.OrdinalIgnoreCase))
                {
                    // The request token can be sent as a JavaScript-readable cookie, 
                    // and Angular uses it by default.
                    var tokens = antiforgery.GetAndStoreTokens(context);

                    //context.Request.Host.Host
                    var coptions = new CookieOptions() { HttpOnly = false, Secure = true, };
                    string url = $"{context.Request.Scheme}://{context.Request.Host.Host}";
                    Uri myUri = new Uri(url);

                    if (myUri.HostNameType == UriHostNameType.Dns && myUri.Host != "localhost")
                    {
                        var indexofdot = myUri.Host.IndexOf('.');
                        coptions.Domain = myUri.Host.Substring(indexofdot);
                    }

                    context.Response.Cookies.Append("XSRF-TOKEN-ACC", tokens.RequestToken, coptions);

                }

                return next(context);
            });

            //app.UseMiddleware<ValidateAntiForgeryTokenMiddleware>();

            //Validates an antiforgery token that was supplied as part of the request.
            app.Use(next => async context =>
            {
                string path = context.Request.Path.Value ?? string.Empty;

                if (string.Equals("POST", context.Request.Method, StringComparison.OrdinalIgnoreCase) && path.StartsWith("/api"))
                {
                    // This will throw if the token is invalid.
                    try
                    {
                        await antiforgery.ValidateRequestAsync(context);
                        await next(context);
                    }
                    catch (AntiforgeryValidationException ex)
                    {
                        logger.LogError(ex, $"context.Request.Path - {path}");
                        context.Response.StatusCode = 400;
                    }
                }
                else
                {
                    await next(context);
                }

            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors(x => x
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 //.WithOrigins("https://*.prudentserviceslocal.com")
                 //.SetIsOriginAllowedToAllowWildcardSubdomains()
                 .SetIsOriginAllowed(origin => true) // allow any origin
                 .AllowCredentials()); // allow credentials
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
