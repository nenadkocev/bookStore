﻿using System;
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
        public int StoreId { get; set; }

        [Required(ErrorMessage = "Полето е задолжително")]
        [Display(Name="Наслов")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Полето е задолжително")]
        public string ISBN { get; set; }
        [Required(ErrorMessage = "Полето е задолжително")]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Полето е задолжително")]
        [Display(Name = "Јазик")]
        public string Language { get; set; }
        public Author Author{ get; set; }
        public Genre Genre{ get; set; }
        [Required(ErrorMessage = "Полето е задолжително")]
        public int Stock { get; set; }
        [Required(ErrorMessage = "Полето е задолжително")]
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