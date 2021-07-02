using Battle.Domain;
using Battle.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Battle.Infrastructure
{
    public class BattleShipService : IBattleShipService
    {
        private readonly IBattleShipRepository battleShipRepo;
        private readonly IBlockRepository blockRepo;
        private readonly ILogger<BattleShipService> logger;

        public BattleShipService(
            IBattleShipRepository battleShipRepo,
            IBlockRepository blockRepo,
            ILogger<BattleShipService> logger)
        {
            this.battleShipRepo = battleShipRepo;
            this.blockRepo = blockRepo;
            this.logger = logger;
        }
        public BattleShip SaveBattleShip(
            int boardId, List<BattleShipBlock> blocksToAssociate)
        {
            logger.LogDebug($"Start - Save new battle ship");

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

            battleShipFromRepo = battleShipRepo.Get(boardId, battleShipFromRepo.Id);

            logger.LogDebug($"End - Save new battle ship");

            return battleShipFromRepo;
        }
    }
}
