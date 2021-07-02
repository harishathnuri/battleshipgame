using System.Collections.Generic;
using System.Linq;

namespace Battle.Domain
{
    public class BattleShip
    {
        public int Id { get; set; }
        public int BoardId { get; set; }
        public virtual Board Board { get; set; }
        public virtual List<BattleShipBlock> BattleShipBlocks { get; set; }
        public ValidationResult Validate()
        {
            var validationResult = new ValidationResult();

            if (BattleShipBlocks?.Count > 0)
            {

                var areBlocksContiguous = AreBlocksContiguous();
                if (areBlocksContiguous == false)
                {
                    validationResult.Status = false;
                    validationResult.Messages.Add("Blocks are not contiguous");
                }
            }
            else
            {
                validationResult.Status = false;
                validationResult.Messages.Add("Battle ship should occupy atleast one block");
            }

            return validationResult;
        }

        public List<Block> GetOverlappingBlocks(List<Block> blocks)
        {
            var overlappingBlocks = new List<Block>();

            if (blocks?.Count > 0)
            {
                var boardBlocks = BattleShipBlocks.Select(b => b.Block);
                overlappingBlocks = boardBlocks.Join(blocks,
                    b => b.Number,
                    nb => nb.Number,
                    (b, nb) => b)
                    .ToList();
            }

            return overlappingBlocks;
        }

        private bool AreBlocksContiguous()
        {
            //logger.LogDebug($"Start - Check for block contiguoity");

            var areBlocksContiguous = true;

            var boardBlocks = BattleShipBlocks.Select(b => b.Block);
            var blockNumbers = boardBlocks.Select(b => b.Number).OrderBy(n => n);
            //horizontal
            areBlocksContiguous = !blockNumbers
                .Select((n, i) => n - i)
                .Distinct()
                .Skip(1)
                .Any();
            var blockBendDetector = blockNumbers
                .Select((n, i) => n / 10)
                .Distinct()
                .Skip(1)
                .Count();

            if (areBlocksContiguous && blockBendDetector >= 1)
            {
                var lastIndexReminder = blockNumbers.Last() % 10;
                if (lastIndexReminder != 0)
                    areBlocksContiguous = false;

                return areBlocksContiguous;
            }

            if (areBlocksContiguous == false)
            {
                //vertical
                areBlocksContiguous = !(blockNumbers
                    .Zip(blockNumbers.Skip(1), (x, y) => y - x)
                    .Distinct()
                    .Skip(1)
                    .Any());
            }

            //logger.LogDebug($"End - Check for block contiguoity");

            return areBlocksContiguous;
        }
    }
}
