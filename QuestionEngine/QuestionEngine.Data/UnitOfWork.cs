﻿using Microsoft.EntityFrameworkCore;
using QuestionEngine.Model.Models;
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

        public UnitOfWork()
        {

        }

        public UnitOfWork(QuestionEngineContext context)
        {
            _context = context;
        }

        public bool AddQuestion(Question question)
        {
            if (question != null)
            {
                if (question.AvailableAnswers != null && question.AvailableAnswers.Count > 0 && question.ChosenAnswerId > 0)
                {

                    try
                    {
                        _context.Questions.Add(question);
                        var test = _context.SaveChanges();
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
            return true;
        }
        public bool DeleteQuestionById(int id)
        {
            return true;
        }
        public Question GetQuestionById(int id)
        {
            return new Question();
        }
    }
}
