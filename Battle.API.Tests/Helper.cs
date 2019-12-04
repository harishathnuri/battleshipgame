using Battle.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Battle.API.Tests
{
    public class Helper
    {
        public const int FAKE_BOARD_ID = 1;

        public static Board FakeBoardFactory()
        {
            return new Board()
            {
                Id = FAKE_BOARD_ID,
                BattleShips = FakeBattleShipFactory(),
                Blocks = FakeBlocksFactory()
            };
        }

        public static Board FakeBoardFactory(
            List<Block> fakeBlocks, List<BattleShip> fakeBattleShips)
        {
            return new Board()
            {
                Id = FAKE_BOARD_ID,
                BattleShips = fakeBattleShips,
                Blocks = fakeBlocks
            };
        }

        public static List<Block> FakeBlocksFactory()
        {
            return Enumerable.Range(1, 100)
                            .Select(n => new Block
                            {
                                Id = n,
                                Number = n
                            })
                            .ToList();
        }

        public static List<BattleShip> FakeBattleShipFactory()
        {
            return new List<List<int>>
                {
                    new List<int> {10, 20, 30, 40 },
                    new List<int> {51, 52, 53, 54 },
                    new List<int> {1, 2, 3, 4 },
                }
                .Select(FakeBattleShipFactory)
                .ToList();
        }

        public static BattleShip FakeBattleShipFactory(
            List<int> blocks, int battleShipId)
        {
            return new BattleShip
            {
                Id = battleShipId,
                BoardId = FAKE_BOARD_ID,
                BattleShipBlocks = blocks.Select((b, bi) =>
                new BattleShipBlock
                {
                    Id = bi,
                    BattleShipId = battleShipId,
                    BlockId = b,
                    Block = new Block
                    {
                        Id = b,
                        Number = b
                    }
                })
                .ToList()
            };
        }
    }
}
