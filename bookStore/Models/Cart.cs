using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bookStore.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public Book book { get; set; }
        public int Quantity { get; set; }

        public Cart(Book Book, int quantity)
        {
            book = Book;
            Quantity = quantity;
        }
    }
}