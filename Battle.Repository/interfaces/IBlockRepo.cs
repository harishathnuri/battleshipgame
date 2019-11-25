using Battle.Domain;
using System.Collections.Generic;

namespace Battle.Repository.Interfaces
{
    public interface IBlockRepo
    {
        List<Block> ListByBoard(int boardId);
        List<BattleShipBlock> ListByBattleShip(int battleshipId);
        List<BattleShipBlock> ListByBattleShip(List<int> battleshipIds);
        List<BattleShipBlock> AssociateBlockToBattleShip(List<BattleShipBlock> blocks);
        List<Block> CreateBlocksForBoard(List<Block> blocks);
    }
}
