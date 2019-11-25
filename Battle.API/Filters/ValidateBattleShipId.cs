using Battle.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Battle.API.Filters
{
    public class ValidateBattleShipId : ActionFilterAttribute
    {
        private readonly IBattleShipRepo battleShipRepo;
        private readonly ILogger<ValidateBoardId> logger;

        public ValidateBattleShipId(IBattleShipRepo battleShipRepo, ILogger<ValidateBoardId> logger)
        {
            this.battleShipRepo = battleShipRepo;
            this.logger = logger;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            logger.LogDebug("Start - OnActionExecuting");

            _ = context.ActionArguments.TryGetValue("boardId", out object foreignKey)
                        && foreignKey is int;
            int boardId = (int)foreignKey;

            if (context.ActionArguments.TryGetValue("Id", out object value)
                        && value is int battleshipId)
            {
                var badRequestResult = new BadRequestObjectResult($"Invalid battle ship id {battleshipId}");
                if (battleshipId <= 0)
                {
                    context.Result = badRequestResult;
                }
                else
                {
                    var board = battleShipRepo.Get(boardId, battleshipId);
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
