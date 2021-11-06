using QuestionEngine.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionEngine.Shared.Rules
{
    public class AvailableAnswerRules : Attribute, IModelRule
    {
        public ValidationResult Validate(string fieldName, object fieldValue)
        {
            var message = "Question must have answers to select";

            List<Answer> availAnswers = (List<Answer>)fieldValue;

            if (availAnswers != null && availAnswers.Count > 0)
            {
                var hasEmptyStrings = availAnswers.Where(x => string.IsNullOrWhiteSpace(x.Description)).Count() > 0;

                if (!hasEmptyStrings)
                    return new ValidationResult() { IsValid = true };

            }

            return new ValidationResult() { IsValid = false, Message = message };

        }
    }

    public class DescriptionRules : Attribute, IModelRule
    {
        public ValidationResult Validate(string fieldName, object fieldValue)
        {
            var message = "Question must have a valid description";

            if (fieldValue != null)
            {
                var desc = (string)fieldValue;

                if (!string.IsNullOrWhiteSpace(desc))
                    return new ValidationResult() { IsValid = true };
            }

            return new ValidationResult() { IsValid = false, Message = message };

        }
    }
}
