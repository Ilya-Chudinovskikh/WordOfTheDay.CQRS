using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entites;

namespace Repository.Context
{
    public class WordsDbContext : DbContext, IWordsDbContext
    {
        public WordsDbContext(DbContextOptions<WordsDbContext> options)
        : base(options)
        {
        }
        public DbSet<Word> Words { get; set; }
        public WordsDbContext()
        {
        }
        public async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Word>()
                .HasIndex(w => new { w.AddTime, w.Email })
                .HasDatabaseName("DateEmail_Index")
                .IsUnique();

            modelBuilder.Entity<Word>()
                .HasIndex(w => new { w.AddTime, w.Text })
                .HasDatabaseName("DateText_Index");
        }
    }
}
