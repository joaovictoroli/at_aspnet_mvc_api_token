using APP_BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_DAL.Interfaces
{
    public interface IAuthorBookRepository
    {
        Task<IEnumerable<AuthorBook>> AddAsync(List<AuthorBook> authorBooks);

        Task<IEnumerable<AuthorBook>> GetAllByBookIdAsync(int bookId);

        Task<IEnumerable<AuthorBook>> GetAllByAuthorIdAsync(int authorId);

        Task DeleteAllLinkedAuthorBooks(int bookId);
    }
}
