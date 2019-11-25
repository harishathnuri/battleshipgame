using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;

namespace Battle.API.Filters
{
    public class ValidateModelState : ActionFilterAttribute
    {
        private readonly ILogger<ValidateModelState> logger;

        public ValidateModelState(ILogger<ValidateModelState> logger)
        {
            this.logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            logger.LogDebug("Entering OnActionExecuting");
            if (!context.ModelState.IsValid)
            {
                string errors = JsonConvert.SerializeObject(context.ModelState.Values
                                    .SelectMany(state => state.Errors)
                                    .Select(error => error.ErrorMessage));
                string arguments = JsonConvert.SerializeObject(context.ActionArguments);
                logger.LogError("Model state is not valid");
                logger.LogError("Following are the action parameters \n {0}", arguments);
                logger.LogError("Following are the errors \n {0}", errors);

                context.Result = new BadRequestObjectResult(context.ModelState);
            }
            logger.LogDebug("Leaving OnActionExecuting");
        }
    }
}
