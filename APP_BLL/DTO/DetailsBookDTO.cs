﻿using APP_BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_BLL.DTO
{
    public class DetailsBookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public DateTime ReleaseDate { get; set; }
        public IEnumerable<AuthorBookDTO> AuthorBook { get; set; }
    }
}
