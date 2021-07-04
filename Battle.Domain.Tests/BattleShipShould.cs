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
        public string Validate_Should_Return_InValid_When_BattleShip_Blocks_Are_Spread_Across_TwoRowsOrColumns(
            List<BattleShipBlock> battleShipBlocks)
        {
            var sut = new BattleShip
            {
                BattleShipBlocks = battleShipBlocks
            };

            var result = sut.Validate();

            return result.Messages.First();
        }

        [TestCase]
        public void Validate_Should_Return_InValid_When_BattleShip_WithNoBlocks()
        {
            var sut = new BattleShip
            {
                BattleShipBlocks = new List<BattleShipBlock>()
            };

            var result = sut.Validate();

            //assert
            Assert.AreEqual(result.Messages.First(), "Battle ship should occupy atleast one block");
        }

        [TestCase]
        public void Validate_Should_Return_InValid_When_BattleShip_WithNullInput()
        {
            var sut = new BattleShip
            {
                BattleShipBlocks = null
            };

            var result = sut.Validate();

            //assert
            Assert.AreEqual(result.Messages.First(), "Battle ship should occupy atleast one block");
        }

        [TestCaseSource("OverlappingBlockTestCases")]
        public List<int> Validate_Should_Return_InVlaid_Overlapping_Blocks_In_BattleShip(
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

                var expectedResult3 = new List<int> { };

                yield return new TestCaseData(Enumerable.Range(11, 5)
                    .Select(n => new BattleShipBlock { Block = new Block { Number = n } })
                    .ToList(), inputBlocks1).Returns(expectedResult1);

                yield return new TestCaseData(new List<int> { 21, 31, 41, 51 }
                    .Select(n => new BattleShipBlock { Block = new Block { Number = n } })
                    .ToList(), inputBlocks2).Returns(expectedResult2);

                yield return new TestCaseData(new List<int> { 21, 31, 41, 51 }
                    .Select(n => new BattleShipBlock { Block = new Block { Number = n } })
                    .ToList(), null).Returns(expectedResult3);
            }
        }
    }
}