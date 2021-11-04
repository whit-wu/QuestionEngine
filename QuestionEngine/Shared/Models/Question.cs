
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionEngine.Shared.Models
{
    public class Question : ModelBase
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        public List<Answer> AvailableAnswers { get; set; }
        

        [Required]
        public int? ChosenAnswerId { get; set; }

    }
}
