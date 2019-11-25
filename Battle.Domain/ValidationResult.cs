using System.Collections.Generic;

namespace Battle.Domain
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            Status = true;
            Messages = new List<string>();
        }

        public bool Status { get; set; }
        public List<string> Messages { get; set; }
    }
}
