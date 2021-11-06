using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionEngine.Shared.Rules
{
    public interface IModelRule
    {
        ValidationResult Validate(String fieldName, object fieldValue);
    }
}
