using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionEngine.Model.Models
{
    public class Survey : ModelBase
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public List<Question> Questions { get; set; }
    }
}
