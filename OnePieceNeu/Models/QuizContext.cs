using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.IO;
namespace OnePieceNeu.Models
{
    public class QuizContext : DbContext
    {

        public DbSet<Frage> Fragen { get; set; }

        public DbSet<Bounty> Bounties { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string ordnerPfad = AppDomain.CurrentDomain.BaseDirectory;
            string datenbankPfad = System.IO.Path.Combine(ordnerPfad, "quiz.db");

            optionsBuilder.UseSqlite($"Data Source={datenbankPfad}");
        }
    }
}