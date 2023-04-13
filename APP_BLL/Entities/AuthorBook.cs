﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_BLL.Entities
{
    public class AuthorBook
    {
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }

        public AuthorBook(int authorId, int bookId)
        {
            AuthorId = authorId;
            BookId = bookId;
        }
    }
}
