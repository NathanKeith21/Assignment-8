using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment5.Models
{
    public class EFLibraryRepository : ILibraryRepository
    {
        private LibraryBookDbContext _context;
        public EFLibraryRepository (LibraryBookDbContext context)
        {
            _context = context;
        }
        public IQueryable<Book> Books => _context.Books;
    }
}
