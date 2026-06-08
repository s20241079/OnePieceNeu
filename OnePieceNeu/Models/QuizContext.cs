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


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            string dbPfad = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "onepiecequiz.db");
            optionsBuilder.UseSqlite($"Data Source={dbPfad}");
        }
    }
}