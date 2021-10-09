using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using QuestionEngine.Data;

namespace QuestionEngine.BackendTests
{
    public class Tests
    {
        DbContextOptions options;
        DbContext context;
        
        [SetUp]
        public void Setup()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            options = new DbContextOptionsBuilder<QuestionEngineContext>().UseSqlite(connection).Options;

            context = new QuestionEngineContext(options: options);
        }
        
        [Test]
        public void TestDBCreation()
        {

            var isDbCreated = context.Database.EnsureCreated();
            Assert.IsTrue(isDbCreated);
        }
    }
}