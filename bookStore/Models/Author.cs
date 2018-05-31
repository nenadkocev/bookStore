using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bookStore.Models
{
    public class Author
    {
        public int Id { get; set; }
        [Required]
        [Display(Name="Автор")]
        public string Name { get; set; }
        public virtual List<Book> Books { get; set; }

        public Author()
        {
            Books = new List<Book>();
        }

        public Author(string name)
        {
            Name = name;
            Books = new List<Book>();
        }
    }
}