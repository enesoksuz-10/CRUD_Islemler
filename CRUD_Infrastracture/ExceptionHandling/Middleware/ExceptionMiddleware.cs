using CRUD_Infrastracture.ExceptionHandling.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace CRUD_Infrastracture.ExceptionHandling.Middleware
{
    // Tüm uygulama boyunca exception yakalar
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        // Request pipeline'ın bir sonraki adımı
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Bir sonraki middleware / controller çalıştırılır
                await _next(context);
            }
            catch (BusinessException ex)
            {
                // Business hataları -> 400 BadRequest
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    message = ex.Message
                };

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(response)
                );
            }
            catch (Exception ex)
            {
                // Beklenmeyen hatalar -> 500 InternalServerError
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    message = "Beklenmeyen bir hata oluştu."
                };

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(response)
                );
            }
        }

        private async Task HandleException(HttpContext context, string message, HttpStatusCode statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = message
            };

            // JSON response yazıyoruz
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
