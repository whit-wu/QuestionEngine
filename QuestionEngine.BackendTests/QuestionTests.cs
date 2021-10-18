using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using QuestionEngine.Data;
using QuestionEngine.Model.Models;
using System;
using System.Collections.Generic;

namespace QuestionEngine.BackendTests
{
    public class QuestionTests
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
        public void AddQuestion_QuestionDescIsNull_ReturnsFalse()
        {
            // Arrange

            var validQuestionToAdd = new Question()
            {
                Id = 1,
                Description = null,
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
            Assert.IsFalse(questionWasAdded);


        }


        [Test]
        public void AddQuestion_QuestionDescIsEmptyString_ReturnsFalse()
        {
            // Arrange

            var validQuestionToAdd = new Question()
            {
                Id = 1,
                Description = string.Empty,
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
            Assert.IsFalse(questionWasAdded);


        }


        [Test]
        public void AddQuestion_QuestionHasEmptyAnswerDescs_ReturnsFalse()
        {
            // Arrange

            var validQuestionToAdd = new Question()
            {
                Id = 1,
                Description = "Will I return false?",
                CreatedBy = userId,
                CreatedOn = DateTime.Now,
                AvailableAnswers = new List<Answer>() {
                    new Answer()
                    {
                        Id = 1,
                        Description = string.Empty,
                        QuestionId = 1,
                        CreatedBy = userId,
                        CreatedOn = DateTime.Now,
                    },
                    new Answer()
                    {
                        Id = 2,
                        Description = string.Empty,
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
            Assert.IsFalse(questionWasAdded);


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

        
        [Test]
        public void UpdateQuestion_UpdateIsValid_ReturnsTrue()
        {
            // Arrange

            // create the question we want to update then save it
            var validQuestionToUpdate = new Question()
            {
                Id = 3,
                Description = "Does updating a valid question work?",
                CreatedBy = userId,
                CreatedOn = DateTime.Now,
                AvailableAnswers = new List<Answer>() {
                    new Answer()
                    {
                        Id = 5,
                        Description = "Yes",
                        QuestionId = 1,
                        CreatedBy = userId,
                        CreatedOn = DateTime.Now,
                    },
                    new Answer()
                    {
                        Id = 6,
                        Description = "No",
                        QuestionId = 1,
                        CreatedBy = userId,
                        CreatedOn = DateTime.Now,
                    }
                },
                ChosenAnswerId = 6
            };

            _context.Questions.Add(validQuestionToUpdate);
            _context.SaveChanges();

            // change a property of that question for the purpose of updating
            validQuestionToUpdate.ChosenAnswerId = 5;

            // Act
            var isUpdated = uow.UpdateQuestion(validQuestionToUpdate);

            // Assert
            Assert.IsTrue(isUpdated);

        }

        [Test]
        public void UpdateQuestion_UpdateIsInvalidBecauseQuestionIsNotFound_ReturnFalse()
        {
            // Arrange
            var question = new Question() {
                Id = 4,
                Description = "Should I be able to update a question that does not exist?",
                CreatedBy = userId,
                CreatedOn = DateTime.Now,
                AvailableAnswers = new List<Answer>() {
                    new Answer()
                    {
                        Id = 7,
                        Description = "Yes",
                        QuestionId = 1,
                        CreatedBy = userId,
                        CreatedOn = DateTime.Now,
                    },
                    new Answer()
                    {
                        Id = 8,
                        Description = "No",
                        QuestionId = 1,
                        CreatedBy = userId,
                        CreatedOn = DateTime.Now,
                    }
                },
                ChosenAnswerId = 8


            };

            // Act 
            var isUpdated = uow.UpdateQuestion(question);

            // Assert
            Assert.IsFalse(isUpdated);
        }


        [Test]
        public void UpdateQuestion_UpdateIsInvalidBecauseChosenAnswerNotFound_ReturnFalse()
        {
            // Arrange
            var question = new Question()
            {
                Id = 5,
                Description = "Should I be able to set an answer that does not exist?",
                CreatedBy = userId,
                CreatedOn = DateTime.Now,
                AvailableAnswers = new List<Answer>() {
                    new Answer()
                    {
                        Id = 9,
                        Description = "Yes",
                        QuestionId = 1,
                        CreatedBy = userId,
                        CreatedOn = DateTime.Now,
                    },
                    new Answer()
                    {
                        Id = 10,
                        Description = "No",
                        QuestionId = 1,
                        CreatedBy = userId,
                        CreatedOn = DateTime.Now,
                    }
                },
                ChosenAnswerId = 10


            };

            _context.Questions.Add(question);
            _context.SaveChanges();

            // change the chosenAnswerId to one that does not exist
            question.ChosenAnswerId = 11;


            // Act 
            var isUpdated = uow.UpdateQuestion(question);

            // Assert
            Assert.IsFalse(isUpdated);
        }

        [Test]
        public void UpdateQuestion_UpdateIsInvalidBecauseChosenAnswerNotTiedToQuestion_ReturnFalse()
        {
            // Arrange
            var question1 = new Question()
            {
                Id = 6,
                Description = "Should I be able to reference an answer tied to another question?",
                CreatedBy = userId,
                CreatedOn = DateTime.Now,
                AvailableAnswers = new List<Answer>() {
                    new Answer()
                    {
                        Id = 12,
                        Description = "Yes",
                        QuestionId = 1,
                        CreatedBy = userId,
                        CreatedOn = DateTime.Now,
                    },
                    new Answer()
                    {
                        Id = 13,
                        Description = "No",
                        QuestionId = 1,
                        CreatedBy = userId,
                        CreatedOn = DateTime.Now,
                    }
                },
                ChosenAnswerId = 13

            };

            // Arrange
            var question2 = new Question()
            {
                Id = 7,
                Description = "Is this really going to work?",
                CreatedBy = userId,
                CreatedOn = DateTime.Now,
                AvailableAnswers = new List<Answer>() {
                    new Answer()
                    {
                        Id = 14,
                        Description = "Yes",
                        QuestionId = 1,
                        CreatedBy = userId,
                        CreatedOn = DateTime.Now,
                    },
                    new Answer()
                    {
                        Id = 15,
                        Description = "No",
                        QuestionId = 1,
                        CreatedBy = userId,
                        CreatedOn = DateTime.Now,
                    }
                },
                ChosenAnswerId = 15

            };


            _context.Questions.Add(question1);
            _context.Questions.Add(question2);
            _context.SaveChanges();

            // change the chosenAnswerId to one that does not exist
            question1.ChosenAnswerId = 15;


            // Act 
            var isUpdated = uow.UpdateQuestion(question1);

            // Assert
            Assert.IsFalse(isUpdated);
        }

        [Test]
        public void DeleteQuestion_FindsQuestionToDelete_ReturnsTrue()
        {
            // Arrange
            var question = new Question()
            {
                Id = 8,
                Description = "Can I delete this question?",
                CreatedBy = userId,
                CreatedOn = DateTime.Now,
                AvailableAnswers = new List<Answer>() {
                    new Answer()
                    {
                        Id = 16,
                        Description = "Yes",
                        QuestionId = 1,
                        CreatedBy = userId,
                        CreatedOn = DateTime.Now,
                    },
                    new Answer()
                    {
                        Id = 17,
                        Description = "No",
                        QuestionId = 1,
                        CreatedBy = userId,
                        CreatedOn = DateTime.Now,
                    }
                },
                ChosenAnswerId = 16

            };

            _context.Questions.Add(question);
            _context.SaveChanges();

            // Act
            var isQuestionDeleted = uow.DeleteQuestionById(8);

            // Assert
            Assert.IsTrue(isQuestionDeleted);
        }

        [Test]
        public void DeleteQuestion_DoesNotFindQuestionToDelete_ReturnsFalse()
        {
            // Arrange
            var questionId = 99;

            // Act
            var isQuestionDeleted = uow.DeleteQuestionById(questionId);

            // Assert
            Assert.IsFalse(isQuestionDeleted);
        }

        [Test]
        public void GetQuestionById_QuestionIsFound_ReturnsQuestion()
        {
            // Arrange
            var question = new Question()
            {
                Id = 9,
                Description = "Can I find this question?",
                CreatedBy = userId,
                CreatedOn = DateTime.Now,
                AvailableAnswers = new List<Answer>() {
                    new Answer()
                    {
                        Id = 17,
                        Description = "Yes",
                        QuestionId = 1,
                        CreatedBy = userId,
                        CreatedOn = DateTime.Now,
                    },
                    new Answer()
                    {
                        Id = 18,
                        Description = "No",
                        QuestionId = 1,
                        CreatedBy = userId,
                        CreatedOn = DateTime.Now,
                    }
                },
                ChosenAnswerId = 17

            };

            _context.Questions.Add(question);
            _context.SaveChanges();

            // Act
            var foundQuestion = uow.GetQuestionById(9);

            // Assert
            Assert.AreEqual(question, foundQuestion);

        }

        [Test]
        public void GetQuestionById_QuestionIsNotFound_ReturnsNull()
        {
            // Arrange
            var questionId = 100;

            // Act
            var foundQuestion = uow.GetQuestionById(questionId);

            // Assert
            Assert.Null(foundQuestion);

        }

    }
}