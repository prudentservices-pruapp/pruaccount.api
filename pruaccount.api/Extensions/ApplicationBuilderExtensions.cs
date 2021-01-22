using Microsoft.AspNetCore.Builder;
using pruaccount.api.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pruaccount.api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder ValidateAntiforgeryTokens(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ValidateAntiForgeryTokenMiddleware>();
        }

        public static IApplicationBuilder UseAntiforgeryToken(this IApplicationBuilder app)
        {
            return app.UseMiddleware<AntiforgeryTokenMiddleware>();
        }
    }
}
