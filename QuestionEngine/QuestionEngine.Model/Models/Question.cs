
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionEngine.Model.Models
{
    public class Question : ModelBase
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public List<Answer> AvailableAnswers { get; } = new List<Answer>();

        public int ChosenAnswerId { get; set; }

    }
}
