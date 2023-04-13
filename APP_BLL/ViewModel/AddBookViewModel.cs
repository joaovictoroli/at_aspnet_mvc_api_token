using APP_BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_BLL.ViewModel
{
    public class AddBookViewModel
    {
        public AddBook addBook { get; set; }

        public List<AddBookAuthorViewModel> authorBooks { get; set; }
    }
}
