using Battle.API.Factories;
using Battle.API.Filters;
using Battle.API.ViewModel;
using Battle.Domain;
using Battle.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Battle.API.Controllers
{
    [Route("/api/board/{boardId}/[controller]")]
    [ApiController]
    [ApiExceptionHandler]
    [TypeFilter(typeof(ValidateBoardId))]
    public class AttackController : Controller
    {
        private readonly IBoardRepo boardRepo;
        private readonly IAttackRepo attackRepo;
        private readonly ILogger<AttackController> logger;

        public AttackController(
            IBoardRepo boardRepo,
            IAttackRepo attackRepo,
            ILogger<AttackController> logger)
        {
            this.boardRepo = boardRepo;
            this.attackRepo = attackRepo;
            this.logger = logger;
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(ValidateModelState))]
        public IActionResult ApiAttackGet(int boardId, int id)
        {
            logger.LogDebug($"Start - Request for attack {id}");

            var attack = attackRepo.Get(boardId, id);
            var response = ResponseFactory.Create(attack);

            logger.LogDebug($"End - Request for attack {id}");

            return Ok(response);
        }

        [HttpPost]
        [TypeFilter(typeof(ValidateModelState))]
        public IActionResult ApiAttackPost(int boardId, BlockToAttack blockToAttack)
        {
            var requestlogger = string.Format(
                    "Start - Request for attack on board {0} and block number {1}",
                    boardId,
                    blockToAttack.Number);
            logger.LogDebug($"Start - {requestlogger}");

            IActionResult result = null;

            var block = new Block
            {
                BoardId = boardId,
                Number = blockToAttack.Number
            };
            // prep board to ask question
            var board = boardRepo.Get(boardId);
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
                attackRepo.Create(attack);
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
