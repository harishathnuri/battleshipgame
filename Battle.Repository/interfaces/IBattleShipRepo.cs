using Battle.Domain;
using System.Collections.Generic;

namespace Battle.Repository.Interfaces
{
    public interface IBattleShipRepo
    {
        BattleShip Get(int boardId, int id);
        List<BattleShip> List(int boardId);
        BattleShip Add(BattleShip battleShip);
    }
}
