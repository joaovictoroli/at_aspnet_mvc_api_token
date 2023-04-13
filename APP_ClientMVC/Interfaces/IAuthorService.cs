using APP_BLL.DTO;
using APP_BLL.Entities;
using APP_BLL.ViewModel;

namespace APP_ClientMVC.Interfaces
{
    public interface IAuthorService
    {
        Task<List<Author>> GetAuthors(HttpContext HttpContext);
        Task<Author> GetAuthorToUpdate(HttpContext HttpContext, int id);
        Task<bool> UpdateAuthor(HttpContext httpContext, UpdateAuthorRequest author, int id);
        Task<bool> DeleteAuthor(HttpContext HttpContext, int authorId);
        Task<Author> AddAuthor(HttpContext httpContext, Author author);
        Task<DetailsAuthorViewModel> GetAuthorDetails(HttpContext httpContext, int id);

    }
}
