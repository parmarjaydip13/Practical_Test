using Application.Interfaces;
using Domain;

namespace Infrastructure.Repository
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(AppDbContext _context) : base(_context)
        {
        }
    }
}
