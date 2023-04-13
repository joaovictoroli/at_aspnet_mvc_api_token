using APP_BLL.Entities;


namespace APP_DAL.Interfaces
{
    public interface IAuthorRepository
    {
        Task<IList<Author>> GetAllAsync();
        Task<Author> GetAsync(int id);
        Task<Author> AddAsync(Author author);
        Task<Author> UpdateAsync(int id, Author author);
        Task<Author> DeleteAsync(int id);
        Task<string> GetFullNameById(int id);

        Task<bool> HasAuthorBook(int id);
    }
}
