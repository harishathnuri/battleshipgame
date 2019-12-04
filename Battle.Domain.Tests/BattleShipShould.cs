using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Battle.Domain.Tests
{
    public class BattleShipShould
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCaseSource("AreContiguousBlockTestCases")]
        public string ReturnInValidWhenBattleShipBlocksAreSpreadAcrossTwoRowsOrColumns(
            List<BattleShipBlock> battleShipBlocks)
        {
            var sut = new BattleShip
            {
                BattleShipBlocks = battleShipBlocks
            };

            var result = sut.Validate();

            return result.Messages.First();
        }

        [TestCaseSource("OverlappingBlockTestCases")]
        public List<int> ReturnInOverlappingBlocksWhenBattleShip(
            List<BattleShipBlock> battleShipBlocks, List<Block> blocks)
        {
            var sut = new BattleShip
            {
                BattleShipBlocks = battleShipBlocks
            };

            var result = sut.GetOverlappingBlocks(blocks);

            return result.Select(b => b.Number).ToList();
        }

        static IEnumerable<TestCaseData> AreContiguousBlockTestCases
        {
            get
            {
                var expectedResult = "Blocks are not contiguous";

                yield return new TestCaseData(Enumerable.Range(8, 4)
                    .Select(n => new BattleShipBlock { Block = new Block { Number = n } })
                    .ToList()).Returns(expectedResult);

                yield return new TestCaseData(new List<int> { 21, 31, 42, 51 }
                    .Select(n => new BattleShipBlock { Block = new Block { Number = n } })
                    .ToList()).Returns(expectedResult);
            }
        }

        static IEnumerable<TestCaseData> OverlappingBlockTestCases
        {
            get
            {
                var inputBlocks1 = new List<int> { 11, 12 }
                    .Select(n => new Block { Number = n })
                    .ToList();
                var inputBlocks2 = new List<int> { 41, 51 }
                    .Select(n => new Block { Number = n })
                    .ToList();

                var expectedResult1 = new List<int> { 11, 12 };

                var expectedResult2 = new List<int> { 41, 51 };

                yield return new TestCaseData(Enumerable.Range(11, 5)
                    .Select(n => new BattleShipBlock { Block = new Block { Number = n } })
                    .ToList(), inputBlocks1).Returns(expectedResult1);

                yield return new TestCaseData(new List<int> { 21, 31, 41, 51 }
                    .Select(n => new BattleShipBlock { Block = new Block { Number = n } })
                    .ToList(), inputBlocks2).Returns(expectedResult2);
            }
        }
    }
}