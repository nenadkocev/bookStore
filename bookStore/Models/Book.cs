using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bookStore.Models
{
    public class Book
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }

        [Required]
        [Display(Name="Наслов")]
        public string Name { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
        [Required]
        [Display(Name = "Јазик")]
        public string Language { get; set; }
        public Author Author{ get; set; }
        public Genre Genre{ get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public string ImageURL { get; set; }
        public Store Store { get; set; }

        public Book(){}

        public Book(string name, string iSBN, decimal price, string language, Author author, Genre genre, Store store)
        {
            Name = name;
            ISBN = iSBN;
            Price = price;
            Language = language;
            Author = author;
            Genre = genre;
            Store = store;
        }
    }
}