using System.Collections.Generic;

namespace Battle.Domain.Interfaces
{
    public interface IBattleShipRepository
    {
        BattleShip Get(int boardId, int id);
        List<BattleShip> List(int boardId);
        BattleShip Add(BattleShip battleShip);
    }
}
