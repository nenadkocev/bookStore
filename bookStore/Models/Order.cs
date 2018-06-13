using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bookStore.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Display(Name = "Број на нарачка")]
        public int OrderKey { get; set; }
        [Display(Name = "Корисничко име")]
        public string Username { get; set; }
        [Display(Name = "Име")]
        public string FirstName { get; set; }
        [Display(Name = "Презиме")]
        public string LastName { get; set; }
        [Display(Name = "Адреса")]
        public string Address { get; set; }
        [Display(Name = "Град")]
        public string City { get; set; }
        [Display(Name = "Провинција")]
        public string State { get; set; }
        [Display(Name = "Поштенски код")]
        public string PostalCode { get; set; }
        [Display(Name = "Држава")]
        public string Country { get; set; }
        [Display(Name = "Мобилен телефон")]
        public string Phone { get; set; }
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        [Display(Name = "Вкупна цена")]
        public string Total { get; set; }
        [Display(Name = "Датум")]
        public System.DateTime OrderDate { get; set; }
        public virtual List<OrderDetails> orderDetails { get; set; }

        public Order()
        {
            orderDetails = new List<OrderDetails>();
        }
    }
}