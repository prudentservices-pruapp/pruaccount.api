using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pruaccount.api.Middleware
{
    public class ValidateAntiForgeryTokenMiddleware
    {
        //The RequestDelegate represents the next middleware in the pipeline.
        private readonly RequestDelegate _next;
        private readonly ILogger<ValidateAntiForgeryTokenMiddleware> _logger;

        public ValidateAntiForgeryTokenMiddleware(RequestDelegate next, ILogger<ValidateAntiForgeryTokenMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, IAntiforgery antiforgery)
        {
            // Call the next delegate/middleware in the pipeline
            string path = context.Request.Path.Value;

            if (HttpMethods.IsPost(context.Request.Method) && !path.StartsWith("/mainctrl"))
            {
                try
                {
                    await antiforgery.ValidateRequestAsync(context);
                    await _next(context);
                }
                catch (AntiforgeryValidationException ex)
                {
                    _logger.LogError(ex, $"ValidateAntiForgeryTokenMiddleware context.Request.Path - {path}");
                    context.Response.StatusCode = 400;
                    return;
                }
            }
            else
            {
                await _next(context);
            }
        }
    }
}
