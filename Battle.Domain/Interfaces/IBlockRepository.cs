using System.Collections.Generic;

namespace Battle.Domain.Interfaces
{
    public interface IBlockRepository
    {
        List<Block> ListByBoard(int boardId);
        List<BattleShipBlock> ListByBattleShip(int battleshipId);
        List<BattleShipBlock> ListByBattleShip(List<int> battleshipIds);
        List<BattleShipBlock> AssociateBlockToBattleShip(List<BattleShipBlock> blocks);
        List<Block> CreateBlocksForBoard(List<Block> blocks);
    }
}
