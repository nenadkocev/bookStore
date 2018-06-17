using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using bookStore.Models;
using Microsoft.AspNet.Identity.Owin;

namespace bookStore.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult BasicIndex()
        {
            var items = db.Orders.Where(x => x.Email == User.Identity.Name);

            return View(items.ToList());
        }

        // GET: Orders
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(db.Orders.ToList());
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult AdminDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Details/5

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if(order == null)
            {
                return new HttpUnauthorizedResult();
            }

            if (order.Email == User.Identity.Name)
            {
                if (order == null)
                {
                    return HttpNotFound();
                }
                return View(order);
            }
            else
            {
                return new HttpUnauthorizedResult();
            }
        }

        // GET: Orders/Create
        [Authorize]
        public ActionResult Create()
        {
            List<Cart> tmp = (List<Cart>)Session["Cart"];
            if(tmp == null || tmp.Count == 0)
            {
                return RedirectToAction("Error");
            }

            Order model = new Order();
            string userName = User.Identity.Name;
            var user = db.Users.FirstOrDefault(u => u.UserName == userName);
            model.Address = user.Address != null ? user.Address : "";
            model.City = user.City != null ? user.City : "";
            model.Country = "Македонија";
            model.Email = user.Email != null ? user.Email : "";
            model.FirstName = user.Name != null ? user.Name : "";
            model.LastName = user.Surname != null ? user.Surname : "";
            model.PostalCode = user.PostalCode != null ? user.PostalCode : "";
            model.State = "Македонија";
            model.Username = user.UserName;
            model.Phone = user.PhoneNumber != null ? user.PhoneNumber : "";
            return View(model);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,OrderKey,Username,FirstName,LastName,Address,City,State,PostalCode,Country,Phone,Email,Total,OrderDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                //string userName = User.Identity.Name;
                //var user = db.Users.FirstOrDefault(u => u.UserName == userName);
                List<Cart> tmp = (List<Cart>)Session["Cart"];
                order.OrderDate = DateTime.Now;
                order.Total = Session["total"].ToString();
                order.Email = User.Identity.Name; //needs fixation
                order.Username = User.Identity.Name;
                Random random = new Random();
                int i = random.Next();
                Session["random"] = i;
                order.OrderKey = i;
                db.Orders.Add(order);
                db.SaveChanges();

                foreach (Cart cart in tmp)
                {
                    OrderDetails o = new OrderDetails
                    {
                        OrderId = order.OrderId,
                        BookId = cart.book.Id,
                        Quantity = cart.Quantity,
                        Price = cart.book.Price.ToString()
                    };

                    db.OrderDetails.Add(o);
                    db.SaveChanges();
                }

                Session.Remove("Cart");
                return RedirectToAction("ShipOut");
            }

            return View(order);
        }

        public ActionResult ShipOut()
        {
            return View();
        }

        // GET: Orders/Edit/5
        [Authorize(Roles = Role.Administrator)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,OrderKey,Username,FirstName,LastName,Address,City,State,PostalCode,Country,Phone,Email,Total,OrderDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        [Authorize(Roles = Role.Administrator)]
        public ActionResult ProcessError()
        {
            return View();
        }

        [Authorize(Roles = Role.Administrator)]
        public ActionResult ProcessFailed(Book o)
        {
            return View(o);
        }

        //GET: Orders/Process/5
        [Authorize(Roles = Role.Administrator)]
        public ActionResult Process(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            foreach(OrderDetails o in order.orderDetails)
            {
                Book book = db.Books.Include(b => b.Store).FirstOrDefault(b => b.Id == o.BookId);
                if(book == null)
                {
                    // probably deleted 
                    return RedirectToAction("ProcessError");
                }
                int stock = book.Stock;
                if(stock - o.Quantity < 0)
                {
                    // cant process the order, return view to tell whick book caused that
                    return RedirectToAction("ProcessFailed", o.Book);
                }
            }
            foreach(OrderDetails o in order.orderDetails)
            {
                Book book = db.Books.Find(o.BookId);
                book.Stock -= o.Quantity;
            }
            db.Orders.Remove(order);
            db.SaveChanges();
            //  success
            return View();
        }

        // GET: Orders/Delete/5
        [Authorize(Roles = Role.Administrator)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}


/*
    admin: martin@admin.com username: kmartin62 password: Password1!
    store: matica@seller.com username: Матица password: Password1!
 */
