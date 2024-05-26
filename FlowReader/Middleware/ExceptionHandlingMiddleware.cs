using FlowReader.Application.Exceptions;
using FlowReader.Core.Exceptions;
using FlowReader.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace FlowReader.Middleware
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly ITempDataProvider _tempDataProvider;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger, ITempDataProvider tempDataProvider)
        {
            _logger = logger;
            _tempDataProvider = tempDataProvider;
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

        private async Task HandleException(HttpContext context, Exception ex)
        {            
            _logger.LogError(ex.Message);

            var code = StatusCodes.Status500InternalServerError;
            var errors = new List<string> { ex.Message };

            code = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                BadRequestException => StatusCodes.Status400BadRequest,
                _ => code
            };
            
            var result = ApiResult.Failure(errors, code);           

            var tempData = _tempDataProvider.LoadTempData(context);
            tempData["ErrorDetails"] = JsonConvert.SerializeObject(result);
            _tempDataProvider.SaveTempData(context, tempData);

            context.Response.Redirect("/Home/Error");
        }
    }
}
