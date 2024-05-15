using FlowReader.Application.Exceptions;
using FlowReader.Application.Models;
using FlowReader.Core.Exceptions;
using Newtonsoft.Json;

namespace FlowReader.Middleware
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private Task HandleException(HttpContext context, Exception ex)
        {
            _logger.LogError(ex.Message);

            var code = StatusCodes.Status500InternalServerError;
            var errors = new List<string> { ex.Message };

            code = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                //ResourceNotFoundException => StatusCodes.Status404NotFound,
                BadRequestException => StatusCodes.Status400BadRequest,
                //UnprocessableRequestException => StatusCodes.Status422UnprocessableEntity,
                _ => code
            };

            var result = JsonConvert.SerializeObject(ApiResult<string>.Failure(errors));

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = code;

            return context.Response.WriteAsync(result);
        }
    }
}
