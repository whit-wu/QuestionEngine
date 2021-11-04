using Microsoft.EntityFrameworkCore;
using QuestionEngine.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionEngine.Data
{
    public interface IUnitOfWork
    {
        public bool AddQuestion(Question question);
        public bool UpdateQuestion(Question question);
        public bool DeleteQuestionById(int id);
        public Question GetQuestionById(int id);
        public bool AddAnswer(Answer answer);
        public bool UpdateAnswer(Answer answer);
        public bool DeleteAnswerById(int id);
        public List<Answer> GetAnswersByQuestionId(int questionId);

    }
}
