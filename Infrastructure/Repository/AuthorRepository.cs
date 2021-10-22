using Application.Interfaces;
using Domain;

namespace Infrastructure.Repository
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(AppDbContext _context) : base(_context)
        {
        }
    }
}
