using Battle.API.Factories;
using Battle.API.Filters;
using Battle.API.ViewModel;
using Battle.Domain;
using Battle.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Battle.API.Controllers
{
    [Route("/api/board/{boardId}/[controller]")]
    [ApiController]
    [ApiExceptionHandler]
    [TypeFilter(typeof(ValidateBoardId))]
    [TypeFilter(typeof(ValidateBattleShipId))]
    public class BattleShipController : Controller
    {
        private readonly IBoardRepo boardRepo;
        private readonly IBattleShipRepo battleShipRepo;
        private readonly IBlockRepo blockRepo;
        private readonly ILogger<BattleShipController> logger;

        public BattleShipController(
            IBoardRepo boardRepo,
            IBattleShipRepo battleShipRepo,
            IBlockRepo blockRepo,
            ILogger<BattleShipController> logger)
        {
            this.boardRepo = boardRepo;
            this.battleShipRepo = battleShipRepo;
            this.blockRepo = blockRepo;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult ApiBattleShipList(int boardId)
        {
            logger.LogDebug($"Start - Request for battle ships");

            var battleShipsFromRepo = battleShipRepo.List(boardId);
            var response = new List<BattleShipResponse>();
            if (battleShipsFromRepo.Count() > 0)
            {
                response = battleShipsFromRepo
                    .Select(ResponseFactory.Create)
                    .ToList();
            }

            logger.LogDebug($"End - Request for battle ships");

            return Ok(response);
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(ValidateModelState))]
        public IActionResult ApiBattleShipGet(int boardId, int id)
        {
            logger.LogDebug($"Start - Request for battle ship {id}");

            var battleShip = battleShipRepo.Get(boardId, id);
            var response = ResponseFactory.Create(battleShip);

            logger.LogDebug($"End - Request for battle ship {id}");

            return Ok(response);
        }

        [HttpPost]
        [TypeFilter(typeof(ValidateModelState))]
        public IActionResult ApiBattleShipPost(int boardId,
            BattleShipToBeCreated battleShipToBeCreated)
        {
            logger.LogDebug("Start - Request for new battle ship");

            IActionResult result = null;

            var blocksToAssociate = battleShipToBeCreated.BlockNumbers
                .Select(n => new BattleShipBlock
                {
                    Block = new Block
                    {
                        BoardId = boardId,
                        Number = n
                    }
                })
                .ToList();
            var battleShip = new BattleShip()
            {
                BoardId = boardId,
                BattleShipBlocks = blocksToAssociate
            };

            // prep board to ask question
            var board = boardRepo.Get(boardId);
            // ask board whether given battle ship can be added
            var battleShipAssociation = board.CanAddBattleShip(battleShip);

            // if can be associated save the battle ship to database
            if (battleShipAssociation.Status == true)
            {
                var battleShipFromRepo = SaveBattleShip(boardId, blocksToAssociate);
                battleShipFromRepo = battleShipRepo.Get(boardId, battleShipFromRepo.Id);
                var successResponse = ResponseFactory.Create(battleShipFromRepo);

                result = CreatedAtAction(nameof(ApiBattleShipGet),
                    new { boardId, id = successResponse.Id }, successResponse);
            }
            else
            {
                // 404 Error
                result = new BadRequestObjectResult(battleShipAssociation);
            }

            logger.LogDebug("End - Request for new battle ship");

            return result;
        }

        private BattleShip SaveBattleShip(
            int boardId, List<BattleShipBlock> blocksToAssociate)
        {
            // construct battle ship to save in repo
            var battleShipToSave = new BattleShip
            {
                BoardId = boardId
            };
            var battleShipFromRepo = battleShipRepo.Add(battleShipToSave);
            // retrieve battle ship Id
            var battleshipId = battleShipFromRepo.Id;
            // construct battle ship blocks
            // retrieve blockIds for given block numbers on board
            var blocksFromRepo = blockRepo.ListByBoard(boardId);
            blocksToAssociate = blocksFromRepo.Join(
                blocksToAssociate,
                br => br.Number,
                bta => bta.Block.Number,
                (br, bta) =>
                     new BattleShipBlock
                     {
                         BattleShipId = battleshipId,
                         BlockId = br.Id
                     })
                .ToList();
            var blocksAssociatedFromRepo =
                blockRepo.AssociateBlockToBattleShip(blocksToAssociate);
            return battleShipFromRepo;
        }
    }
}