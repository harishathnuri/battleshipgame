using System.Collections.Generic;
using System.Linq;

namespace Battle.Domain
{
    public class Board
    {
        public int Id { get; set; }
        public virtual List<Block> Blocks { get; set; }
        public virtual List<BattleShip> BattleShips { get; set; }

        public int Length { get; }
        public int Breadth { get; }
        public int FirstBlockNumber { get; }
        public int LastBlockNumber { get; }
        public Board()
        {
            Length = 10;
            Breadth = 10;
            FirstBlockNumber = 1;
            LastBlockNumber = 100;
        }

        public ValidationResult CanAddBattleShip(BattleShip battleShip)
        {
            var validationResult = new ValidationResult();

            if (battleShip == null)
            {
                validationResult.Status = false;
                validationResult.Messages.AddRange(new List<string> { "Please add valid battleship" });
                return validationResult;
            }

            var battleShipValidationResult = battleShip.Validate();

            if (battleShipValidationResult.Status == false)
            {
                validationResult.Status = false;
                validationResult.Messages.AddRange(battleShipValidationResult.Messages);
                return validationResult;
            }

            var newBattleShipBlocks = battleShip.BattleShipBlocks.Select(b => b.Block).ToList();
            var overlappingBlocks = GetOverlappingBlocksFromAllBattleShips(newBattleShipBlocks);

            var areBlocksOverlapping = overlappingBlocks.Any();
            if (areBlocksOverlapping == true)
            {
                var message = $"Following blocks are occupied {string.Join(",", overlappingBlocks.Select(b => b.Number))}";
                validationResult.Status = false;
                validationResult.Messages.Add(message);
            }

            return validationResult;
        }

        public AttackResult CanAttackBlock(Block blockToAttack)
        {
            var blocksToAttack = new List<Block> { blockToAttack };
            return CanAttackBlocks(blocksToAttack);
        }

        public AttackResult CanAttackBlocks(List<Block> blocksToAttack)
        {
            var attackResult = new AttackResult();

            var blocksUnderAttack = GetOverlappingBlocksFromAllBattleShips(blocksToAttack);

            if (blocksUnderAttack.Any())
            {
                attackResult.Status = true;
                attackResult.Message = "Success...";
            }
            else
            {
                attackResult.Status = false;
                attackResult.Message = "Failed...";
            }

            return attackResult;
        }

        private List<Block> GetOverlappingBlocksFromAllBattleShips(List<Block> blocks)
        {
            var overlappingBlocks = new List<Block>();
            foreach (var ship in BattleShips)
            {
                var currentOverLappingBlocks = ship.GetOverlappingBlocks(blocks);
                if (currentOverLappingBlocks.Any())
                {
                    overlappingBlocks.AddRange(currentOverLappingBlocks);
                }
            }

            return overlappingBlocks;
        }
    }
}
