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
    }
}
