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

        public ValidateAntiForgeryTokenMiddleware(ILogger<ValidateAntiForgeryTokenMiddleware> logger)
        {
            _logger = logger;
        }

        public ValidateAntiForgeryTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task InvokeAsync(HttpContext context)
        {
            // Call the next delegate/middleware in the pipeline
            string path = context.Request.Path.Value;

            if (HttpMethods.IsPost(context.Request.Method) || HttpMethods.IsPut(context.Request.Method) || HttpMethods.IsDelete(context.Request.Method))
            {
                if (context.Request.Cookies["XSRF-TOKEN"] == null)
                {
                    _logger.LogError($"context.Request.Path - {path} did not pass XSRF-TOKEN cookie");
                    context.Response.StatusCode = 400;
                }
            }
            return this._next(context);
        }
    }
}
