
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionEngine.Data.Model
{
    public class Question
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public List<Answer> AvailableAnswers { get; } = new List<Answer>();
        public DateTime CreatedOn { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedBy { get; set; }

    }
}
