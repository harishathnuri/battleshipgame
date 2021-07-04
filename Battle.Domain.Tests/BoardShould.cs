using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Battle.Domain.Tests
{
    public class BoardShould
    {
        [SetUp]
        public void Setup()
        {

        }

        [TestCase]
        public void CanAddBattleShip_Should_Return_False_For_Null_BattleShip()
        {
            var sut = new Board()
            {
                BattleShips = new List<BattleShip>()
            };

            var result = sut.CanAddBattleShip(null);

            //assert
            Assert.AreEqual(result.Status, false);
        }

        [TestCase]
        public void CanAddBattleShip_Should_Return_False_For_Invalid_BattleShip()
        {
            var sut = new Board()
            {
                BattleShips = new List<BattleShip>()
            };

            var result = sut.CanAddBattleShip(new BattleShip());

            //assert
            Assert.AreEqual(result.Status, false);
        }

        [TestCase]
        public void CanAddBattleShip_Should_Return_False_For_Overlapping_BattleShip()
        {
            var sut = new Board()
            {
                BattleShips = new List<BattleShip>()
                {
                    new BattleShip()
                    {
                        BattleShipBlocks = new List<int> { 21, 31, 41, 51 }
                            .Select(n => new BattleShipBlock { Block = new Block { Number = n } })
                            .ToList()
                    }

                }
            };

            var battleShipToAdd = new BattleShip()
            {
                BattleShipBlocks = new List<int> { 21, 31 }
                            .Select(n => new BattleShipBlock { Block = new Block { Number = n } })
                            .ToList()
            };

            var result = sut.CanAddBattleShip(battleShipToAdd);

            //assert
            Assert.AreEqual(result.Status, false);
        }

        [TestCase]
        public void CanAttackBlocks_Should_Return_True_For_Occupied_BattleShip()
        {
            var sut = new Board()
            {
                BattleShips = new List<BattleShip>()
                {
                    new BattleShip()
                    {
                        BattleShipBlocks = new List<int> { 21, 31, 41, 51 }
                            .Select(n => new BattleShipBlock { Block = new Block { Number = n } })
                            .ToList()
                    }

                }
            };

            var blocks = new List<int> { 21, 31 }
                        .Select(n => new Block { Number = n })
                        .ToList();

            var result = sut.CanAttackBlocks(blocks);

            //assert
            Assert.AreEqual(result.Status, true);
        }

        [TestCase]
        public void CanAttackBlocks_Should_Return_True_For_Empty_BattleShip()
        {
            var sut = new Board()
            {
                BattleShips = new List<BattleShip>()
                {
                    new BattleShip()
                    {
                        BattleShipBlocks = new List<int> { 21, 31, 41, 51 }
                            .Select(n => new BattleShipBlock { Block = new Block { Number = n } })
                            .ToList()
                    }

                }
            };

            var blocks = new List<int> { 61, 71 }
                        .Select(n => new Block { Number = n })
                        .ToList();

            var result = sut.CanAttackBlocks(blocks);

            //assert
            Assert.AreEqual(result.Status, false);
        }
    }
}
