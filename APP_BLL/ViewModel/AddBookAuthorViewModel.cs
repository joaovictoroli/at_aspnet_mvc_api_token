using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_BLL.ViewModel
{
    public class AddBookAuthorViewModel
    {
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public string FullName {  get; set; }
    }
}
