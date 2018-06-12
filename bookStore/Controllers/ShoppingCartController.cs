using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bookStore.Models;

namespace bookStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private string strCart = "Cart";
        // GET: ShoppingCart
        public ActionResult EmptyCart()
        {
            return View();
        }

        public ActionResult Index()
        {
            List<Cart> cart = (List<Cart>)Session[strCart];
            if (Session[strCart] == null || cart.Count == 0)
            {
                return View("EmptyCart");
            }
            else
            {

                return View();
            }
        }

        public int Total()
        {
            List<Cart> cart = (List<Cart>)Session["Cart"];

            return cart.Count;
        }

        public PartialViewResult Shop()
        {
            int i = 0;
            List<Cart> cart = (List<Cart>)Session[strCart];
            if (cart != null)
            {
                i = cart.Count;
            }
            ViewBag.inforCart = i;
            return PartialView("shop");
        }

        public ActionResult OrderNow(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            if (Session[strCart] == null)
            {
                List<Cart> lsCart = new List<Cart>
                {
                    new Cart(db.Books.Find(id), 1)
                };

                Session[strCart] = lsCart;
            }
            else
            {
                List<Cart> lsCart = (List<Cart>)Session[strCart];
                int check = isExistingCheck(id);
                if (check == -1)
                {
                    lsCart.Add(new Cart(db.Books.Find(id), 1));
                }
                else
                {
                    lsCart[check].Quantity++;
                }
                Session[strCart] = lsCart;
            }

            return View("Index");
        }

        private int isExistingCheck(int? id)
        {
            List<Cart> lsCart = (List<Cart>)Session[strCart];

            for (int i = 0; i < lsCart.Count; i++)
            {
                if (lsCart[i].book.Id == id) return i;
            }

            return -1;
        }

        [HttpDelete]
        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            List<Cart> cart = (List<Cart>)Session[strCart];
            if (cart.Count != 0)
            {
                int check = isExistingCheck(id);

                cart.RemoveAt(check);
            }

            return RedirectToAction("Index");
        }

        
    }
}