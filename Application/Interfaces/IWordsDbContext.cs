using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Domain.Entites;

namespace Application.Interfaces
{
    public interface IWordsDbContext
    {
        DbSet<Word> Words { get; set; }
        Task SaveChanges();
    }
}
