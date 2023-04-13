using APP_BLL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_BLL.ViewModel
{
    public class EditBookViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Isbn { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public IEnumerable<int> AuthorsId { get; set; }

        public List<AddBookAuthorViewModel> AuthorBook { get; set; }
    }
}
