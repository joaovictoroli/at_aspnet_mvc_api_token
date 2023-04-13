using APP_BLL.Entities.Common;

namespace APP_BLL.Entities
{
    public class Author : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public virtual IList<AuthorBook> AuthorBook { get; set; }
    }
}
