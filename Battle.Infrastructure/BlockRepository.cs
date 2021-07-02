using Battle.Domain;
using Battle.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Battle.Infrastructure
{
    public class BlockRepository : IBlockRepository
    {
        private readonly BattleAppContext context;
        private readonly ILogger<BlockRepository> logger;
        public BlockRepository(
            BattleAppContext context, ILogger<BlockRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public List<BattleShipBlock> AssociateBlockToBattleShip(List<BattleShipBlock> blocks)
        {
            context.BattleShipBlocks.AddRange(blocks);
            context.SaveChanges();
            return blocks;
        }

        public List<Block> CreateBlocksForBoard(List<Block> blocks)
        {
            context.AddRange(blocks);
            context.SaveChanges();
            return blocks;
        }

        public List<BattleShipBlock> ListByBattleShip(int battleshipId)
        {
            var blocks = context.BattleShipBlocks
                .Include(bb => bb.Block)
                .Where(b => b.BattleShipId == battleshipId)
                .ToList();
            return blocks;
        }

        public List<BattleShipBlock> ListByBattleShip(List<int> battleshipIds)
        {
            var blocks = context.BattleShipBlocks
                .Include(bb => bb.Block)
                .Where(b => battleshipIds.Contains(b.BattleShipId))
                .ToList();
            return blocks;
        }

        public List<Block> ListByBoard(int boardId)
        {
            var blocks = context.Blocks
                .Where(b => b.BoardId == boardId)
                .ToList();
            return blocks;
        }
    }
}
