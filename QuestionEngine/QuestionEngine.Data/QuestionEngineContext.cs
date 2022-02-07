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
using QuestionEngine.Shared.Models;

namespace QuestionEngine.Data
{
    public class QuestionEngineContext : DbContext
    {
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        public string DbPath { get; private set; }

        public bool IsUnitTestContext { get; set; }

        private readonly string connectionString;



        public QuestionEngineContext(string connectionString) 
        {
            if (connectionString != null) {
                this.connectionString = connectionString;
            }
        }

        public QuestionEngineContext(bool isUnitTestContext)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}QuestionEngine.db";
            IsUnitTestContext = isUnitTestContext;
           
        }

     

        public QuestionEngineContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
      
        }
        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>()
                .HasIndex(q => q.Id)
                .IsUnique();

            modelBuilder.Entity<Answer>()
                .HasIndex(a => a.Id)
                .IsUnique();

            modelBuilder.Entity<Survey>()
                .HasIndex(s => s.Id)
                .IsUnique();
        }


    }
}
