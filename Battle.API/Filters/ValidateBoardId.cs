using Battle.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Battle.API.Filters
{
    public class ValidateBoardId : ActionFilterAttribute
    {
        private readonly IBoardRepo boardRepo;
        private readonly ILogger<ValidateBoardId> logger;

        public ValidateBoardId(IBoardRepo boardRepo, ILogger<ValidateBoardId> logger)
        {
            this.boardRepo = boardRepo;
            this.logger = logger;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            logger.LogDebug("Start - OnActionExecuting");

            int boardId = 0;
            var isBoardIdAvailable = context.ActionArguments.TryGetValue("boardId", out object foreignKey)
                        && foreignKey is int;
            if (foreignKey != null)
            {
                boardId = (int)foreignKey;
            }
            if (!isBoardIdAvailable)
            {
                isBoardIdAvailable = context.ActionArguments.TryGetValue("Id", out object primaryKey)
                        && primaryKey is int;
                if (primaryKey != null)
                {
                    boardId = (int)primaryKey;
                }
            }
            if (isBoardIdAvailable)
            {
                var badRequestResult = new BadRequestObjectResult($"Invalid board id {boardId}");
                if (boardId <= 0)
                {
                    context.Result = badRequestResult;
                }
                else
                {
                    var board = boardRepo.Get(boardId);
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
