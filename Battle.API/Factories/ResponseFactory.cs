using Battle.API.ViewModel;
using Battle.Domain;
using System.Linq;

namespace Battle.API.Factories
{
    public class ResponseFactory
    {
        public static BattleShipResponse Create(BattleShip battleShipFromRepo)
        {
            return new BattleShipResponse
            {
                Id = battleShipFromRepo.Id,
                Blocks = battleShipFromRepo
                    .BattleShipBlocks
                    .Select(bb => new BattleShipBlockResponse
                    {
                        Id = bb.Id,
                        Number = bb.Block.Number
                    })
                    .ToList()
            };
        }

        public static AttackResponse Create(Attack attackFromRepo)
        {
            return new AttackResponse
            {
                Id = attackFromRepo.Id,
                Number = attackFromRepo.Block.Number,
                BoardId = attackFromRepo.Block.BoardId
            };
        }

        public static BlockResponse Create(Block blockFromRepo)
        {
            return new BlockResponse
            {
                Id = blockFromRepo.Id,
                Number = blockFromRepo.Number,
            };
        }

        public static BoardResponse Create(Board boardFromRepo)
        {
            return new BoardResponse
            {
                Id = boardFromRepo.Id,
                BattleShips = boardFromRepo.BattleShips.Select(Create).ToList(),
                Blocks = boardFromRepo.Blocks.Select(Create).ToList()
            };
        }
    }
}
