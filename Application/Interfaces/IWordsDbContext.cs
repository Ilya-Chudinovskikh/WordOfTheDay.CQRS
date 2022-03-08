using System.Threading.Tasks;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IWordsDbContext
    {
        DbSet<Word> Words { get; set; }
        Task SaveChanges();
    }
}
