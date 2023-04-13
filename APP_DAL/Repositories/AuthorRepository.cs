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
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _applicationDbContext;
        public AuthorRepository(AppDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Author> AddAsync(Author author)
        {
            await _applicationDbContext.Authors.AddAsync(author);
            await _applicationDbContext.SaveChangesAsync();
            return author;
        }

        public async Task<Author> DeleteAsync(int id)
        {
            var author = await _applicationDbContext.Authors.FirstOrDefaultAsync(p => p.Id == id);

            if (author == null) { return null; }

            _applicationDbContext.Remove(author);
            await _applicationDbContext.SaveChangesAsync();
            return author;
        }

        public async Task<IList<Author>> GetAllAsync()
        {
            return await _applicationDbContext.Authors.ToListAsync();
        }

        public async Task<Author> GetAsync(int id)
        {
            return await _applicationDbContext.Authors.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<string> GetFullNameById(int id)
        {
            var author = await _applicationDbContext.Authors.FirstOrDefaultAsync(p => p.Id == id);
            return $"{author.FirstName} {author.LastName}";
        }

        public async Task<bool> HasAuthorBook(int id)
        {
            var list = await _applicationDbContext.AuthorBooks.Where(b => b.AuthorId == id).ToListAsync();
            if ((list != null) && (!list.Any()))
            {
                return false;
            }
            return true;
        }

        public async Task<Author> UpdateAsync(int id, Author author)
        {
            var existingUser = await _applicationDbContext.Authors.FirstOrDefaultAsync(p => p.Id == id);

            if (existingUser == null) { return null; }

            existingUser.FirstName = author.FirstName;
            existingUser.LastName = author.LastName;
            existingUser.Email = author.Email;
            existingUser.BirthDate = author.BirthDate;

            await _applicationDbContext.SaveChangesAsync();
            return existingUser;
        }
    }
}
