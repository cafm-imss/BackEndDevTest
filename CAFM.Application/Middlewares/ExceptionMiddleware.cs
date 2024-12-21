using CAFM.Application.Responses;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;

namespace CAFM.Application.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                await HandleValidationException(context, ex);
            }
            catch (Exception ex)
            {
                await HandleGenericException(context, ex);
            }
        }

        private async Task HandleValidationException(HttpContext context, ValidationException ex)
        {
            var result = ex.Errors.Select(a => a.ErrorMessage).ToList();

            context.Response.ContentType = "application/json";

            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
        }

        private async Task HandleGenericException(HttpContext context, Exception ex)
        {
            var result = new BaseResponse<Exception>
            {
                Errors = new List<string>
                {
                    ex.Message,
#if DEBUG
                    ex.InnerException?.Message ?? string.Empty,
                    ex.StackTrace ?? string.Empty
#endif
                }
            };

            context.Response.ContentType = "application/json";

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(result.Errors));
        }
    }
}
