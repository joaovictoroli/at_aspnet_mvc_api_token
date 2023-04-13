using APP_BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_DAL.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book> GetAsync(int id);
        Task<Book> AddAsync(Book book);
        Task<Book> UpdateAsync(int id, Book book);
        Task<Book> DeleteAsync(int id);
        Task<string> GetIsbnById(int id);
    }
}
