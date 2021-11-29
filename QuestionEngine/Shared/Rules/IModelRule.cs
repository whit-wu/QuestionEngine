using System;

namespace QuestionEngine.Shared.Rules
{
    public interface IModelRule
    {
        ValidationResult Validate(String fieldName, object fieldValue);
    }
}
