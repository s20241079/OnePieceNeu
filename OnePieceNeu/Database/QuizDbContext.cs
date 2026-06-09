using Microsoft.EntityFrameworkCore;
using OnePieceNeu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePieceNeu.Database
{
    public class QuizDbContext : DbContext
    {
        public DbSet<Frage> Fragen { get; set; }

        public DbSet<Bounty> Bounties { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=quiz.db");
        }
    }
}
