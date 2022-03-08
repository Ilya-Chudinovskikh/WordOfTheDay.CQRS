using Application.Features.WordFeatures.Queries.QueryExtensions;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.WordFeatures.Queries
{
    public class EmailIsAlreadyUsedQuery : IRequest<bool>
    {
        public string Email { get; set; }
        public EmailIsAlreadyUsedQuery(string email)
        {
            Email = email;
        }
        public class EmailIsAlreadyUsedQueryHadler : IRequestHandler<EmailIsAlreadyUsedQuery, bool>
        {
            private readonly IWordsDbContext _context;
            private readonly IDateTodayService _dateToday;
            public EmailIsAlreadyUsedQueryHadler(IWordsDbContext context, IDateTodayService dateToday)
            {
                _context = context;
                _dateToday = dateToday;
            }
            public async Task<bool> Handle(EmailIsAlreadyUsedQuery query, CancellationToken cancellationToken)
            {
                var exist = await _context.Words
                    .LaterThan(_dateToday.DateToday)
                    .AnyAsync(word => word.Email == query.Email, cancellationToken);

                return exist;
            }
        }
    }
}
