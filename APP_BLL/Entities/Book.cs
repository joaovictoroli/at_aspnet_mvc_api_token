using APP_BLL.Entities.Common;

namespace APP_BLL.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string Isbn { get; set; }
        public DateTime ReleaseDate { get; set; }
        public virtual IEnumerable<AuthorBook> AuthorBook { get; set; }
    }
}
