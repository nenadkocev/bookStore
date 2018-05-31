using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bookStore.Models
{
    public class Genre
    {
        public int Id { get; set; }
        [Required]
        [Display(Name="Жанр")]
        public string Name { get; set; }
        public virtual List<Book> Books { get; set; }

        public Genre()
        {
            Books = new List<Book>();
        }

        public Genre(string name)
        {
            Name = name;
            Books = new List<Book>();
        }
    }
}