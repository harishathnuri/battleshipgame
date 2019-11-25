using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Battle.API.ViewModel
{
    public class BattleShipToBeCreated : IValidatableObject
    {
        public List<int> BlockNumbers { get; set; }

        public IEnumerable<ValidationResult> Validate(
            ValidationContext validationContext)
        {
            if (!(BlockNumbers?.Count > 0))
            {
                yield return new ValidationResult(
                    "Battle ship should be atleast of one block size",
                    new string[] { nameof(BlockNumbers) });
            }
            if ((BlockNumbers?.Count > 10))
            {
                yield return new ValidationResult(
                    "Battle ship should be maximum ten block size",
                    new string[] { nameof(BlockNumbers) });
            }
        }
    }
}
