using Battle.Domain;
using System.Collections.Generic;

namespace Battle.Repository.Interfaces
{
    public interface IBattleShipService
    {
        BattleShip SaveBattleShip(
            int boardId, List<BattleShipBlock> blocksToAssociate);
    }
}
