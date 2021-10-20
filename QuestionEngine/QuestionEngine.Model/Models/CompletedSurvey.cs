using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionEngine.Model.Models
{
    public class CompletedSurvey : ModelBase
    {
        public int Id { get; set; }
        public Survey Survey { get; set; }

    }
}
