using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Battle.Domain.Tests
{
    public class BoardShould
    {
        [TestCase]
        public void CanAddBattleShipShouldReturnFalseForNullBattleShip()
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
        public void CanAddBattleShipShouldReturnFalseForInvalidBattleShip()
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
        public void CanAddBattleShipShouldReturnFalseForOverlappingBattleShip()
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
        public void CanAttackBlocksShouldReturnTrueForOccupiedBattleShip()
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
        public void CanAttackBlocksShouldReturnTrueForEmptyBattleShip()
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
