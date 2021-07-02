using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Battle.API.Filters
{
    public class ValidatePrimaryKey : ActionFilterAttribute
    {
        private readonly ILogger<ValidateBoardId> logger;

        public ValidatePrimaryKey(ILogger<ValidateBoardId> logger)
        {
            this.logger = logger;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
