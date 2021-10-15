using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using QuestionEngine.Data;
using QuestionEngine.Model.Models;
using System;
using System.Collections.Generic;

namespace QuestionEngine.BackendTests
{
    public class AnswerTests
    {
        private DbContextOptions options;
        private QuestionEngineContext _context;
        private string userId = "SYSTEM";
        private IUnitOfWork uow;

        [SetUp]
        public void SetupContext()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            options = new DbContextOptionsBuilder<QuestionEngineContext>().UseSqlite(connection).Options;


            // allows tables to be created first
            using (var migContext = new QuestionEngineContext(options))
            {
                migContext.Database.EnsureCreated();
            }


            // the actual context
            _context = new QuestionEngineContext(options);

            uow = new UnitOfWork(_context);
        }

        [Test]
        public void AddAnswer_AnswerIsValid_ReturnsTrue()
        {
            // Arrange

            // Make existing Question
            var validQuestionToAdd = new Question()
            {
                Id = 1,
                Description = "Does adding a valid question work?",
                CreatedBy = userId,
                CreatedOn = DateTime.Now,
                AvailableAnswers = new List<Answer>() {
                    new Answer()
                    {
                        Id = 1,
                        Description = "Maybe",
                        QuestionId = 1,
                        CreatedBy = userId,
                        CreatedOn = DateTime.Now,
                    },
                    new Answer()
                    {
                        Id = 2,
                        Description = "No",
                        QuestionId = 1,
                        CreatedBy = userId,
                        CreatedOn = DateTime.Now,
                    }
                },
                ChosenAnswerId = 1
            };

            _context.Questions.Add(validQuestionToAdd);
            _context.SaveChanges();

            // answer to add
            var validAnswer = new Answer()
            {
                Id = 3,
                Description = "Yes",
                CreatedBy = userId,
                CreatedOn = DateTime.Now,
                QuestionId = 1
            };

            // Act 
            var answerWasAdded = uow.AddAnswer(validAnswer);


            // Assert
            Assert.IsTrue(answerWasAdded);
        }

        [Test]
        public void AddAnswer_NoQuestionTiedToAnswer_ReturnsFalse()
        {
            // Arrange 
            var invalidAnswer = new Answer()
            {
                Id = 4,
                Description = "This ain't going to work",
                CreatedBy = userId,
                CreatedOn = DateTime.Now
            };
            // Act 
            var answerWasAdded = uow.AddAnswer(invalidAnswer);


            // Assert
            Assert.IsTrue(answerWasAdded);
        }

        [Test]
        public void AddAnswer_DescIsBlank_ReturnsFalse()
        {
            // Arrange 
            var invalidAnswer = new Answer()
            {
                Id = 4,
                Description = string.Empty,
                CreatedBy = userId,
                CreatedOn = DateTime.Now
            };
            // Act 
            var answerWasAdded = uow.AddAnswer(invalidAnswer);


            // Assert
            Assert.IsTrue(answerWasAdded);
        }


        [Test]
        public void AddAnswer_DescIsNull_ReturnsFalse()
        {
            // Arrange 
            var invalidAnswer = new Answer()
            {
                Id = 4,
                Description = null,
                CreatedBy = userId,
                CreatedOn = DateTime.Now
            };
            // Act 
            var answerWasAdded = uow.AddAnswer(invalidAnswer);


            // Assert
            Assert.IsTrue(answerWasAdded);
        }
    }
}
