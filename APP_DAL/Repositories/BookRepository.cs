using APP_BLL.DTO;
using APP_BLL.Entities;
using APP_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_DAL.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _applicationDbContext;

        private readonly IAuthorBookRepository _authorBookRepository;
        public BookRepository(AppDbContext applicationDbContext, IAuthorBookRepository authorBookRepository)
        {
            _applicationDbContext = applicationDbContext;
            _authorBookRepository = authorBookRepository;
        }

        public async Task<Book> AddAsync(Book book)
        {
            await _applicationDbContext.Books.AddAsync(book);
            await _applicationDbContext.SaveChangesAsync();
            return book;
        }

        public async Task<Book> DeleteAsync(int id)
        {
            var book = await _applicationDbContext.Books.FirstOrDefaultAsync(p => p.Id == id);

            if (book == null) { return null; }

            _applicationDbContext.Remove(book);
            await _applicationDbContext.SaveChangesAsync();
            return book;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            var listBooks = await _applicationDbContext.Books.ToListAsync();     

            return listBooks;
        }

        public async Task<Book> GetAsync(int id)
        {
            return await _applicationDbContext.Books.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<string> GetIsbnById(int id)
        {
            var book = await _applicationDbContext.Books.FirstOrDefaultAsync(p => p.Id == id);
            return $"{book.Isbn}";
        }

        public async Task<Book> UpdateAsync(int id, Book book)
        {
            var existingBook = await _applicationDbContext.Books.FirstOrDefaultAsync(p => p.Id == id);

            if (existingBook == null) { return null; }

            existingBook.Title = book.Title;
            existingBook.Isbn = book.Isbn;
            existingBook.ReleaseDate = book.ReleaseDate;

            await _applicationDbContext.SaveChangesAsync();
            return existingBook;
        }
    }
}
