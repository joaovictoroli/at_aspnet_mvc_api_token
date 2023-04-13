using APP_BLL.DTO;
using APP_BLL.Entities;
using APP_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace APP_DAL.Repositories
{
    public class AuthorBookRepository : IAuthorBookRepository
    {
        private readonly AppDbContext _applicationDbContext;
       
        public AuthorBookRepository(AppDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<AuthorBook>> AddAsync(List<AuthorBook> authorBooks)
        {
            await _applicationDbContext.AuthorBooks.AddRangeAsync(authorBooks);
            await _applicationDbContext.SaveChangesAsync();
            return authorBooks;
        }

        public async Task<IEnumerable<AuthorBook>> GetAllByBookIdAsync(int bookId)
        {
            var authorBooks = await _applicationDbContext.AuthorBooks.Where(b => b.BookId == bookId).ToListAsync();
            return authorBooks;
        }

        public async Task DeleteAllLinkedAuthorBooks(int bookId)
        {
            var authorBooks = await _applicationDbContext.AuthorBooks.Where(b => b.BookId == bookId).ToListAsync();
            _applicationDbContext.AuthorBooks.RemoveRange(authorBooks);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<AuthorBook>> GetAllByAuthorIdAsync(int authorId)
        {
            var authorBooks = await _applicationDbContext.AuthorBooks.Where(b => b.AuthorId == authorId).ToListAsync();
            return authorBooks;
        }
    }
}
