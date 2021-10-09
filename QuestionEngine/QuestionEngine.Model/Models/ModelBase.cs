using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionEngine.Model.Models
{
    public class ModelBase
    {
        public DateTime CreatedOn { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
