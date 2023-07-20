﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DotnetCoding.Services.Exceptions
{
    public class ApplicationExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IExceptionHandler _exceptionHandler;
        private readonly ILogger<ApplicationExceptionMiddleware> _logger;

        public ApplicationExceptionMiddleware(
            RequestDelegate next,
            IExceptionHandler exceptionHandler,
            ILogger<ApplicationExceptionMiddleware> logger)
        {
            _next = next;
            _exceptionHandler = exceptionHandler;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var error = _exceptionHandler.HandleException(ex);
                if (!httpContext.Response.HasStarted)
                {
                  
                    httpContext.Response.ContentType = MediaTypeNames.Application.Json;
                    httpContext.Response.StatusCode = (int)error.StatusCode;
                    await httpContext.Response.WriteAsync(JsonSerializer.Serialize(
                        error.Title));
                }
            }
        }
    }
}
