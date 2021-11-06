using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionEngine.Shared.Rules
{
    public class QuestionIdRules: Attribute, IModelRule
    {
        public ValidationResult Validate(string fieldName, object fieldValue)
        {
            var message = "Answer must be tied to a question";

            int? questionId = fieldValue != null ? (int)fieldValue : null;

            if (questionId == null)
                return new ValidationResult() { IsValid = false, Message = message };

            return new ValidationResult() { IsValid = true };
        }
    }

    public class AnswerDescRules: Attribute, IModelRule
    {
        public ValidationResult Validate(string fieldName, object fieldValue)
        {
            var message = "Answer must have valid description";

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
