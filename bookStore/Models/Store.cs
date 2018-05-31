using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bookStore.Models
{
    public class Store
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string TelephoneNumber { get; set; }
        [EmailAddress(ErrorMessage = "Невалидна е-маил адреса")]
        public string Email { get; set; }

        public virtual List<Book> Books { get; set; }
        public virtual List<Book> Orders { get; set; }

        public Store()
        {
            Books = new List<Book>();
            Orders = new List<Book>();
        }

        public Store(string name, string address, string telephoneNumber, string email)
        {
            Name = name;
            Address = address;
            TelephoneNumber = telephoneNumber;
            Email = email;
            Books = new List<Book>();
            Orders = new List<Book>();
        }
    }
}