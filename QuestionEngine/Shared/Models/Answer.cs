using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestionEngine.Shared.Rules;

namespace QuestionEngine.Shared.Models
{
    public class Answer : ModelBase
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [AnswerDescRules]
        public string Description { get; set; }
        [Required]
        [QuestionIdRules]
        public int? QuestionId { get; set; }

    }
}
