using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace UpStart.Middlewares
{
    public class ResponseExceptionHandler : Controller
    {
        private readonly RequestDelegate _next;

        public ResponseExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (FluentValidation.ValidationException ex)
            {
                await HandleValidationExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleValidationExceptionAsync(HttpContext context, FluentValidation.ValidationException exception, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            var errors = new List<string>();
            foreach (var erro in exception.Errors)
            {
                errors.Add(erro.ErrorMessage);
            }
            //errors.Add(exception.Message);
            if (exception.InnerException != null)
            {
                GetInnerException(exception.InnerException, errors);
            }
            var responseEntity = new
            {
                success = false,
                errors = errors.ToArray()
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(responseEntity));
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            var errors = new List<string>();
            errors.Add(exception.Message);
            if (exception.InnerException != null)
            {
                GetInnerException(exception.InnerException, errors);
            }
            var responseEntity = new
            {
                success = false,
                errors = errors.ToArray()
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(responseEntity));
        }

        private static void GetInnerException(Exception exception, List<string> errors)
        {
            if (exception is ApiException)
                errors.Add(((ApiException)exception).Content);
            else
                errors.Add(exception.Message);
            if (exception.InnerException != null)
            {
                GetInnerException(exception.InnerException, errors);
            }
        }

        public bool IsProduction()
        {
            return Environment.GetEnvironmentVariable("Ambiente") == "PROD";
        }

    }

    public static class ResponseExceptionHandlerExtensions
    {
        public static IApplicationBuilder UseResponseExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseExceptionHandler>();
        }
    }
}
