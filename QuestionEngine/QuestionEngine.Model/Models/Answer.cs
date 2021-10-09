using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionEngine.Model.Models
{
    public class Answer : ModelBase
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int QuestionId { get; set; }

    }
}
