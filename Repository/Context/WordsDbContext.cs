using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
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
        public async Task SaveChanges()
        {
            await base.SaveChangesAsync();
        }
    }
}
