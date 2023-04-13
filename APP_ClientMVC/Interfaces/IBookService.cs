using APP_BLL.DTO;
using APP_BLL.Entities;
using APP_BLL.ViewModel;

namespace APP_ClientMVC.Interfaces
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooks(HttpContext HttpContext);
        Task<AddBook> AddBook(HttpContext httpContext, AddBook addBook);
        Task DeleteBook(HttpContext httpContext, int bookId);
        Task<DetailsBookViewModel> GetBookDetails(HttpContext httpContext, int bookId);
        Task<DetailsBookViewModel> GetBookToUpdate(HttpContext HttpContext, int bookId);
        Task<bool> UpdateBookRequest(HttpContext httpContext, UpdateBookRequest book);

    }
}
