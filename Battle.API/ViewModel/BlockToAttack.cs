using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Battle.API.ViewModel
{
    public class BlockToAttack : IValidatableObject
    {
        public int Number { get; set; }

        public IEnumerable<ValidationResult> Validate(
            ValidationContext validationContext)
        {
            if (Number <= 0 || Number > 100)
            {
                yield return new ValidationResult(
                    "Block number should be between 1 and 100",
                    new string[] { nameof(Number) });
            }
        }
    }
}
