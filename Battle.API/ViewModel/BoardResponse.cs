using System.Collections.Generic;

namespace Battle.API.ViewModel
{
    public class BoardResponse
    {
        public int Id { get; set; }
        public List<BattleShipResponse> BattleShips { get; set; }
        public List<BlockResponse> Blocks { get; set; }
    }
}
