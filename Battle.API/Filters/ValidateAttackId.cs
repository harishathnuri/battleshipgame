using Battle.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Battle.API.Filters
{
    public class ValidateAttackId : ActionFilterAttribute
    {
        private readonly IAttackRepo attackRepo;
        private readonly ILogger<ValidateAttackId> logger;

        public ValidateAttackId(IAttackRepo attackRepo, ILogger<ValidateAttackId> logger)
        {
            this.attackRepo = attackRepo;
            this.logger = logger;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            logger.LogDebug("Start - OnActionExecuting");

            _ = context.ActionArguments.TryGetValue("boardId", out object foreignKey)
                        && foreignKey is int;
            int boardId = (int)foreignKey;

            if (context.ActionArguments.TryGetValue("Id", out object value)
                        && value is int attackId)
            {
                var badRequestResult = new BadRequestObjectResult($"Invalid attack id {attackId}");
                if (attackId <= 0)
                {
                    context.Result = badRequestResult;
                }
                else
                {
                    var board = attackRepo.Get(boardId, attackId);
                    if (board == null)
                    {
                        context.Result = badRequestResult;
                    }
                }
            }

            logger.LogDebug("End - OnActionExecuting");
        }
    }
}
