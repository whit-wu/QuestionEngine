using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using QuestionEngine.Data;
using QuestionEngine.Model.Models;
using System;
using System.Collections.Generic;

namespace QuestionEngine.BackendTests
{
    public class Tests
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
        public void AddQuestion_QuestionToAddIsValid_ReturnsTrue()
        {
            // Arrange
            
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
                        Description = "Yes",
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

            // Act
            var questionWasAdded = uow.AddQuestion(validQuestionToAdd);

            // Assert
            Assert.IsTrue(questionWasAdded);


        }

        [Test]
        public void AddQuestion_QuestionToAddIsInvalidBecauseThereIsNoChosenAnswer_ReturnsFalse()
        {
            // Arrange

            var invalidQuestionToAdd = new Question()
            {
                Id = 2,
                Description = "Does adding an invalid question work?",
                CreatedBy = userId,
                CreatedOn = DateTime.Now,
                AvailableAnswers = new List<Answer>() {
                    new Answer()
                    {
                        Id = 3,
                        Description = "Yes",
                        QuestionId = 1,
                        CreatedBy = userId,
                        CreatedOn = DateTime.Now,
                    },
                    new Answer()
                    {
                        Id = 4,
                        Description = "No",
                        QuestionId = 1,
                        CreatedBy = userId,
                        CreatedOn = DateTime.Now,
                    }
                },
            };

            // Act
            var questionWasAdded = uow.AddQuestion(invalidQuestionToAdd);

            // Assert
            Assert.IsFalse(questionWasAdded);
        }

        [Test]
        public void AddQuestion_QuestionToAddIsInvalidBecauseNoAvailableAnswers_ReturnsFalse()
        {
            // Arrange

            var invalidQuestionToAdd = new Question()
            {
                Id = 2,
                Description = "Does adding an invalid question work?",
                CreatedBy = userId,
                CreatedOn = DateTime.Now,
                AvailableAnswers = new List<Answer>(),
            };

            // Act
            var questionWasAdded = uow.AddQuestion(invalidQuestionToAdd);

            // Assert
            Assert.IsFalse(questionWasAdded);
        }

    }
}