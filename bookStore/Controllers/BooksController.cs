using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using bookStore.Models;
using System.Linq.Dynamic;

namespace bookStore.Controllers
{
    public class BooksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Books
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult getBooks()
        {

            // Server side parameter
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];

            var books = db.Books.Select(b =>
            new
            {
                Name = b.Name,
                Author = b.Author.Name,
                Genre = b.Genre.Name,
                Price = b.Price,
                Language = b.Language,
                ImageURL = b.ImageURL,
                AuthorId = b.AuthorId,
                GenreId = b.GenreId,
                Id = b.Id
            });

            int totalRows = books.Count();

            //  Filter
            if (!String.IsNullOrEmpty(searchValue))
            {
                books = books
                    .Where(book =>
                    book.Name.ToLower().Contains(searchValue.ToLower()) ||
                    book.Author.ToLower().Contains(searchValue.ToLower()) ||
                    book.Genre.ToLower().Contains(searchValue.ToLower())
                    );
            }
            int totalRowsAfterFiltering = books.Count();
            //  Sort
            sortDirection = sortDirection == "asc" ? "ascending" : "descending";
            books = books.AsQueryable().OrderBy(sortColumnName + " " + sortDirection);

            // Paging
            books = books.Skip(start).Take(length);

            return Json(new { data = books, draw = Request["draw"], recordsTotal = totalRows, recordsFiltered = totalRowsAfterFiltering}, JsonRequestBehavior.AllowGet);
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Include(b => b.Author).Include(b => b.Genre).FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            //ViewBag.AuthorId = new SelectList(db.Authors, "Id", "Name");
            //ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name");
            return View();
        }

        public ActionResult getAuthors(string term)
        {
            var authors = db.Authors
                .Where(a => a.Name.ToLower().Contains(term.ToLower()))
                .Select(a => new { label = a.Name, Id = a.Id});
            return Json(authors, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getGenres(string term)
        {
            var genres = db.Genres
                .Where(a => a.Name.ToLower().Contains(term.ToLower()))
                .Select(a => new { label = a.Name, Id = a.Id });
            return Json(genres, JsonRequestBehavior.AllowGet);
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AuthorId,GenreId,Name,ISBN,Price,Language,Stock,ImageURL")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "Name", book.AuthorId);
            ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name", book.GenreId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AuthorId,GenreId,Name,ISBN,Price,Language,Stock,ImageURL")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "Name", book.AuthorId);
            ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name", book.GenreId);
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
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
