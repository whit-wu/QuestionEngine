using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using QuestionEngine.Data;
using QuestionEngine.Shared.Models;
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
            using (var migContext = new QuestionEngineContext(options, true))
            {
                migContext.Database.EnsureCreated();
            }


            // the actual context
            _context = new QuestionEngineContext(options, true);

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
            Assert.IsFalse(answerWasAdded);
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
            Assert.IsFalse(answerWasAdded);
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
            Assert.IsFalse(answerWasAdded);
        }

        [Test]
        public void UpdateAnswer_AnswerIsValid_ReturnsTrue()
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


            // Act
            validQuestionToAdd.AvailableAnswers[0].Description = "Yes";
            var isUpdated = uow.UpdateAnswer(validQuestionToAdd.AvailableAnswers[0]);

            // Assert
            Assert.IsTrue(isUpdated);
        }

        [Test]
        public void UpdateAnswer_NoQuestionIdFoundForQuestion_ReturnsFalse()
        {
            // Arrange
            var answer = new Answer()
            {
                Id = 1,
                CreatedBy = userId,
                CreatedOn = DateTime.Now,
                Description = "No",
                QuestionId = 99
            };

            // Act
            var isUpdated = uow.UpdateAnswer(answer);

            // Assert
            Assert.IsFalse(isUpdated);

        }

        [Test]
        public void UpdateAnswer_AnswerDescMissing_ReturnsFalse()
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


            // Act
            validQuestionToAdd.AvailableAnswers[0].Description = string.Empty;
            var isUpdated = uow.UpdateAnswer(validQuestionToAdd.AvailableAnswers[0]);

            // Assert
            Assert.IsFalse(isUpdated);
        }

        [Test]
        public void DeleteAnswerById_AnswerIsRemoved_ReturnsTrue()
        {
            // Arrange
            var validQuestionToAdd = new Question()
            {
                Id = 1,
                Description = "Does removing a valid answer work?",
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

            _context.Questions.Add(validQuestionToAdd);
            _context.SaveChanges();

            // Act 
            var isRemoved = uow.DeleteAnswerById(1);

            // Assert 
            Assert.IsTrue(isRemoved);

        }

        [Test]
        public void DeleteAnswerById_AnswerIsNotFound_ReturnsFalse()
        {
            // Arrange
            int answerIdThatDoesNotExist = 900;

            // Act
            var isRemoved = uow.DeleteAnswerById(answerIdThatDoesNotExist);

            // Assert
            Assert.IsFalse(isRemoved);

        }

        [Test]
        public void GetAnswersByQuestionId_FindsAnswers_RetunsListOfAnswers()
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

            // Act
            var listOfAnswers = uow.GetAnswersByQuestionId(1);

            // Assert
            Assert.AreEqual(listOfAnswers, validQuestionToAdd.AvailableAnswers);
        }

        [Test]
        public void GetAnswersByQuestionId_DoesNotFindsAnswers_RetunsEmptyList()
        {
            // Arrange
            int questionIdThatDoesNotExist = 32423;

            // Act
            var listOfAnswers = uow.GetAnswersByQuestionId(questionIdThatDoesNotExist);

            // Assert
            Assert.IsTrue(listOfAnswers.Count == 0);
        }
    }
}
