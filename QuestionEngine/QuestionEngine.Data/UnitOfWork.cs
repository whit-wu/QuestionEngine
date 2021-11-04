using Microsoft.EntityFrameworkCore;
using QuestionEngine.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionEngine.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private QuestionEngineContext _context;


        public UnitOfWork(QuestionEngineContext context)
        {
            _context = context;
        }

        public bool AddQuestion(Question question)
        {
            if (question != null)
            {
                if (question.AvailableAnswers != null 
                    && question.AvailableAnswers.Count > 0  
                    && !string.IsNullOrWhiteSpace(question.Description))
                {

                    // check if answers are empty strings
                    var hasEmptyStrings = question.AvailableAnswers.Where(x => string.IsNullOrWhiteSpace(x.Description)).Count();

                    if (hasEmptyStrings > 0)
                            return false;

                    try
                    {
                        _context.Questions.Add(question);
                        _context.SaveChanges();
                        return true;
                    }
                    catch (Exception ex)
                    {

                        return false;
                    }
                    
                    
                }
            }
            return false;
        }

        public bool UpdateQuestion(Question question)
        {
            var questionToUpdate = _context.Questions
                .Where(q => question.Id == q.Id)
                .FirstOrDefault();

            if (questionToUpdate != null && question.AvailableAnswers != null && question.AvailableAnswers.Count > 0)
            {
                var availableAnswerIds = question.AvailableAnswers.Select(a => a.Id).ToArray();
                
                if (!availableAnswerIds.Contains((int)question.ChosenAnswerId))
                    return false;

                question = questionToUpdate;

                try
                {
                    _context.SaveChanges();
                    return true;
                }
                catch(Exception ex)
                {
                    return false;
                }
                
                    
            }
            
            return false;
        }
        public bool DeleteQuestionById(int id)
        {
            var questionToRemove = _context.Questions.Where(q => q.Id == id).FirstOrDefault();

            if (questionToRemove != null)
            {
                try
                {
                    _context.Questions.Remove(questionToRemove);
                    _context.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {

                    return false;
                }


            }

            return false;
        }
        public Question GetQuestionById(int id)
        {
            return _context.Questions.Where(q => q.Id == id).FirstOrDefault();
        }

        public bool AddAnswer(Answer answer)
        {
            if (answer.QuestionId != null && !string.IsNullOrWhiteSpace(answer.Description))
            {
                try
                {
                    _context.Answers.Add(answer);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {

                    return false;
                }
            }
            return false;
        }

        public bool UpdateAnswer(Answer answer)
        {
            if (!string.IsNullOrWhiteSpace(answer.Description) && answer.QuestionId != null)
            {
                try
                {
                    _context.Answers.Update(answer);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {

                    return false;
                }
            }

            return false;
        }

        public bool DeleteAnswerById(int id)
        {
            try
            {
                var answerToRemove = _context.Answers.Find(id);
                if (answerToRemove != null)
                {
                    _context.Answers.Remove(answerToRemove);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {

                return false;
            }

            return false;
        }

        public List<Answer> GetAnswersByQuestionId(int questionId)
        {
            return _context.Answers.Where(x => x.QuestionId == questionId).ToList();
        }

        
    }
}
