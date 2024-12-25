using BusinessPortal.Application.UseCases.Commons.Bases;
using BusinessPortal.Application.UseCases.Commons.Exceptions;
using System.Text.Json;

namespace BusinessPortal.WebApi.Extensions.Middleware
{
    public class ValidationMiddleWare
    {
        private readonly RequestDelegate _next;

        public ValidationMiddleWare(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (ValidationExceptionCustom ex)
            {
                context.Response.ContentType = "application/json";
                await JsonSerializer.SerializeAsync(context.Response.Body, new BaseResponse<object> { Message = "Validation Errors", Errors = ex.Errors });
            }
        }
    }
}
