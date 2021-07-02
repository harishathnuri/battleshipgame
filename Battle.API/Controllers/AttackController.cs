using Battle.API.Factories;
using Battle.API.Filters;
using Battle.API.ViewModel;
using Battle.Domain;
using Battle.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Battle.API.Controllers
{
    [Route("/api/board/{boardId}/[controller]")]
    [ApiController]
    [ApiExceptionHandler]
    [TypeFilter(typeof(ValidateBoardId))]
    [TypeFilter(typeof(ValidateAttackId))]
    public class AttackController : Controller
    {
        private readonly IBoardRepository boardRepository;
        private readonly IAttackRepository attackRepository;
        private readonly ILogger<AttackController> logger;

        public AttackController(
            IBoardRepository boardRepository,
            IAttackRepository attackRepository,
            ILogger<AttackController> logger)
        {
            this.boardRepository = boardRepository;
            this.attackRepository = attackRepository;
            this.logger = logger;
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(ValidateModelState))]
        public IActionResult ApiAttackGet(int boardId, int id)
        {
            logger.LogDebug($"Start - Request for attack {id}");

            var attack = attackRepository.Get(boardId, id);
            var response = ResponseFactory.Create(attack);

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

            ObjectResult result = null;

            var block = new Block
            {
                BoardId = boardId,
                Number = blockToAttack.Number
            };
            // prep board to ask question
            var board = boardRepository.Get(boardId);
            // ask board whether given block can be attacked
            var attackResult = board.CanAttackBlock(block);

            // if attack is successful save the entity in database
            if (attackResult.Status == true)
            {
                var blockUnderAttack =
                    board.Blocks.First(b => b.Number == blockToAttack.Number);
                var attack = new Attack
                {
                    BlockId = blockUnderAttack.Id
                };
                attackRepository.Create(attack);
                result = CreatedAtAction(nameof(ApiAttackGet),
                    new { boardId, id = attack.Id }, attackResult);
            }
            else
            {
                // 404
                result = new BadRequestObjectResult(attackResult);
            }

            logger.LogDebug($"End - {requestlogger}");

            return result;
        }
    }
}
