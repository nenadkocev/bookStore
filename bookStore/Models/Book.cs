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
        [Display(Name="Име")]
        public string Name { get; set; }
        public string ISBN { get; set; }
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
        [Display(Name = "Јазик")]
        public string Language { get; set; }
        public Author Author{ get; set; }
        public Genre Genre{ get; set; }
        public int Stock { get; set; }
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