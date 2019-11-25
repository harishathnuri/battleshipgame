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

            if (context.ActionArguments.TryGetValue("boardId", out object value)                
                        && value is int boardId)
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
