using Battle.API.Factories;
using Battle.API.Filters;
using Battle.API.ViewModel;
using Battle.Application.Commands;
using Battle.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Battle.API.Controllers
{
    [Route("/api/board/{boardId}/[controller]")]
    [ApiController]
    [ApiExceptionHandler]
    [TypeFilter(typeof(ValidateBoardId))]
    [TypeFilter(typeof(ValidateAttackId))]
    public class AttackController : Controller
    {
        private readonly IQuery<RetrieveAttackRequest, RetrieveAttackResponse> retrieveAttackQuery;
        private readonly ICommand<CreateAttackRequest, CreateAttackResponse> createAttackCommand;
        private readonly ILogger<AttackController> logger;

        public AttackController(
            IQuery<RetrieveAttackRequest, RetrieveAttackResponse> retrieveAttackQuery,
            ICommand<CreateAttackRequest, CreateAttackResponse> createAttackCommand,
            ILogger<AttackController> logger)
        {
            this.retrieveAttackQuery = retrieveAttackQuery;
            this.createAttackCommand = createAttackCommand;
            this.logger = logger;
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(ValidateModelState))]
        public IActionResult ApiAttackGet(int boardId, int id)
        {
            logger.LogDebug($"Start - Request for attack {id}");

            var retrieveAttackRequest = new RetrieveAttackRequest()
            {
                BoardId = boardId,
                AttackId = id
            };
            var retrieveAttackReponse = retrieveAttackQuery.Execute(retrieveAttackRequest);
            var response = ResponseFactory.Create(retrieveAttackReponse.Attack);

            logger.LogDebug($"End - Request for attack {id}");

            return Ok(response);
        }

        [HttpPost]
        [TypeFilter(typeof(ValidateModelState))]
        public ObjectResult ApiAttackPost(int boardId, BlockToAttackRequest blockToAttack)
        {
            var requestlogger = string.Format(
                    "Start - Request for attack on board {0} and block number {1}",
                    boardId,
                    blockToAttack.Number);
            logger.LogDebug($"Start - {requestlogger}");

            var createAttackRequest = new CreateAttackRequest()
            {
                BoardId = boardId,
                BlockNumber = blockToAttack.Number
            };
            var createAttackResponse = createAttackCommand.Execute(createAttackRequest);

            ObjectResult result = null;
            if (createAttackResponse.AttackResult.Status == true)
            {
                result = CreatedAtAction(nameof(ApiAttackGet),
                    new
                    {
                        boardId,
                        id = createAttackResponse.AttackId
                    },
                    createAttackResponse.AttackResult);
            }
            else
            {
                // 404
                result = new BadRequestObjectResult(createAttackResponse.AttackResult);
            }

            logger.LogDebug($"End - {requestlogger}");

            return result;
        }
    }
}
