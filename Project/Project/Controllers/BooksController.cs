using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Project.Models;

namespace Project.Controllers
{
    public class BooksController : Controller
    {
        private ProjectContext db = new ProjectContext();

        // GET: Books
        public ActionResult Index(string searchString, string sortOrder, string currentFilter, string bookWriter, string bookGenre, int? page)
        {
            var books = db.Books.Include(b => b.Genre).Include(b => b.Writer);

            var GenreList = new List<string>();

            var GenreQue = from d in db.Books
                           orderby d.Genre.GenreName
                           select d.Genre.GenreName;

            GenreList.AddRange(GenreQue.Distinct());

            ViewBag.bookGenre = new SelectList(GenreList);
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "Title" : "";
            ViewBag.GenreSort = String.IsNullOrEmpty(sortOrder) ? "Genre" : "";

            if (searchString != null)

            {
                page = 1;
            }

            else

            {
                searchString = currentFilter;

            }

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(b => b.Title.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(bookWriter))
            {
                books = books.Where(b => b.Writer.FirstName.Contains(bookWriter));
            }

            if (!String.IsNullOrEmpty(bookGenre))
            {
                books = books.Where(b => b.Genre.GenreName == bookGenre);
            }

            switch (sortOrder)
            {
                case "Title":
                    books = books.OrderBy(b => b.Title);
                    break;
                case "Genre":
                    books = books.OrderByDescending(b => b.Genre.GenreName);
                    break;
                default:
                    books = books.OrderByDescending(b => b.Title);
                    break;
            }

            int pageSize = 10;
            int pageNum = (page ?? 1);

            return View(books.ToPagedList(pageNum, pageSize));


        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
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

        // GET: Books/Create
        public ActionResult Create()
        {
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName");
            ViewBag.WriterID = new SelectList(db.Writers, "WriterID", "FirstName");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookID,Title,ReleaseDate,WriterID,GenreID,Description")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName", book.GenreID);
            ViewBag.WriterID = new SelectList(db.Writers, "WriterID", "FirstName", book.WriterID);
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
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName", book.GenreID);
            ViewBag.WriterID = new SelectList(db.Writers, "WriterID", "FirstName", book.WriterID);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookID,Title,ReleaseDate,WriterID,GenreID,Description")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName", book.GenreID);
            ViewBag.WriterID = new SelectList(db.Writers, "WriterID", "FirstName", book.WriterID);
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
