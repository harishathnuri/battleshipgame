using System.Collections.Generic;

namespace Battle.API.ViewModel
{
    public class BattleShipResponse
    {
        public int Id { get; set; }
        public List<BattleShipBlockResponse> Blocks { get; set; }
    }
}
