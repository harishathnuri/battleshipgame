using System.Collections.Generic;

namespace Battle.Domain.Interfaces
{
    public interface IBattleShipService
    {
        BattleShip SaveBattleShip(
            int boardId, List<BattleShipBlock> blocksToAssociate);
    }
}
