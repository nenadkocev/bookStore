using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bookStore.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Total { get; set; }
        public virtual List<Book> Books { get; set; }

        public ShoppingCart()
        {
            Total = 0;
            Books = new List<Book>();
        }

        public void addBook(Book book)
        {
            Books.Add(book);
        }

        public void calculateTotal()
        {
            Total = Books.Select(book => book.Price).Sum();
        }

        public void clearCart()
        {
            Books.RemoveAll(b => true);
        }

        public void removeBook(int id)
        {
            Book toRemove = Books.Find(b => b.Id == id);
            if (toRemove != null)
                Books.Remove(toRemove);
        }
    }
}