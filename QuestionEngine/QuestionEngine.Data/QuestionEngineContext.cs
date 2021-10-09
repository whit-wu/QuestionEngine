using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using QuestionEngine.Model.Models;

namespace QuestionEngine.Data
{
    public class QuestionEngineContext : DbContext
    {
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        public string DbPath { get; private set; }

        private readonly string connectionString;



        public QuestionEngineContext(string connectionString) 
        {
            if (connectionString != null) {
                this.connectionString = connectionString;
            }
        }

        public QuestionEngineContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // if we are running the whole app, use sql server
            if (!options.IsConfigured)
            {
                options.UseSqlServer(connectionString);
            }
        }


        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");

            connection.Open();

            return connection;
        }


    }
}
