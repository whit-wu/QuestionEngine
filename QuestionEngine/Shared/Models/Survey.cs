using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionEngine.Shared.Models
{
    // for questions and answers, we make the uow do all the legwork
    // for validation, and in the backend.  let's see what happens
    // when we introduce a shared lib that can do validation on
    // both front and back ends
    public class Survey : ModelBase
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }


        public List<Question> Questions { get; set; }
    }
}
