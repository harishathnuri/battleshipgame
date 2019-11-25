using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Battle.API.Filters
{
    public class ApiExceptionHandler : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
            var response = context.HttpContext.Response;
            response.ContentType = "application/json";

            response.StatusCode = 500;
            var result = JsonConvert.SerializeObject(
                new
                {
                    isError = true,
                    errorMessage = "Internal server error"
                });

            response.ContentLength = result.Length;
            response.WriteAsync(result);
        }
    }
}
