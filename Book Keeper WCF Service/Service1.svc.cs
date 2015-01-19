using Book_Keeper_WCF_Service.Database;
using Book_Keeper_WCF_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Book_Keeper_WCF_Service
{
    public class Service1 : IService1
    {
        /**
         * Combines books and authors from the database into a model object list that is returned
         *
         * @return IEnumerable<BookModel>
         */
        public IEnumerable<BookModel> GetBooks()
        {
            BookKeeperEntities db = new BookKeeperEntities();

            var books = db.Books.Where(q => q.Hidden == false);

            List<BookModel> bookModels = new List<BookModel>();
            foreach (var book in books)
            {
                var authors = (from ba in db.BookXAuthors
                               join a in db.Authors on ba.Authorid equals a.Authorid
                               where ba.Bookid == book.Bookid && ba.Hidden == false && a.Hidden == false
                               select new AuthorModel { Autherid = a.Authorid, Name = a.Name}
                              ).ToList();
                bookModels.Add(new BookModel { Authors = authors, Bookid = book.Bookid, Price = book.Price, Stock = book.Stock, Title = book.Title, Description = book.Description, Note = book.Note });
            }

            db.Dispose();

            return bookModels;
        }

        /**
         * Selects Authors from the database into a model that is returned
         *
         * @return AuthorModel
         */
        public IEnumerable<AuthorModel> getAuthors()
        {
            BookKeeperEntities db = new BookKeeperEntities();

            var authors = (from a in db.Authors
                           where a.Hidden == false
                           select new AuthorModel { Autherid = a.Authorid,  Name = a.Name}).ToList();

            db.Dispose();

            return authors;
        }

        /**
         * Selects a single book by ID from the database combines it with authors into a model object that is returned
         *
         * @return BookModel
         */
        public BookModel GetBookById(int id)
        {
            BookKeeperEntities db = new BookKeeperEntities();

            var book = db.Books.Where(q => q.Hidden == false && q.Bookid == id).First();

            var authors = (from ba in db.BookXAuthors
                           join a in db.Authors on ba.Authorid equals a.Authorid
                           where ba.Bookid == book.Bookid && ba.Hidden == false && a.Hidden == false
                           select new AuthorModel { Autherid = a.Authorid, Name = a.Name }
                          ).ToList();

            db.Dispose();

            return new BookModel { Authors = authors, Bookid = book.Bookid, Price = book.Price, Stock = book.Stock, Title = book.Title, Description = book.Description, Note = book.Note };
        }

        /**
         * Selects a single author by ID from the database into a model object that is returned
         *
         * @return AuthorModel
         */
        public AuthorModel GetAuthorById(int id)
        {
            BookKeeperEntities db = new BookKeeperEntities();

            var authors = (from a in db.Authors
                           where a.Hidden == false && a.Authorid == id
                           select new AuthorModel { Autherid = a.Authorid, Name = a.Name }).First();

            db.Dispose();

            return authors;
        }

        /**
         * Adds an book to the database
         *
         * @return bool
         */
        public bool AddBook(BookModel book)
        {
            BookKeeperEntities db = new BookKeeperEntities();

            var bookdb = db.Books.Add(new Book { Hidden = false, Price = book.Price, Stock = book.Stock, Title = book.Title, Description = book.Description, Note = book.Note });
            
            foreach (AuthorModel author in book.Authors)
            {
                var authordb = db.Authors.Add(new Author { Hidden = false, Name = author.Name });
                db.BookXAuthors.Add(new BookXAuthor { Bookid = bookdb.Bookid, Authorid = authordb.Authorid });
            }

            db.SaveChanges();

            return true;
        }

        /**
         * Deletes books & authors from the database
         *
         * @return bool
         */
        public bool DeleteBookByID(int id)
        {
            BookKeeperEntities db = new BookKeeperEntities();

            //Selecting the book by id from the database and setting hidden to true
            var book = (from b in db.Books
                        where b.Bookid == id
                        select b).First();

            book.Hidden = true;

            //Using the bookid select all relationships to book/authors and set hidden to true
            var bookAuthors = (from ba in db.BookXAuthors
                               where ba.Bookid == book.Bookid
                               select ba).ToList();

            foreach (var bookAuthor in bookAuthors)
            {
                bookAuthor.Hidden = true;
                var author = (from a in db.Authors
                              where a.Authorid == bookAuthor.Authorid
                              select a).First();
                author.Hidden = true;
            }

            db.SaveChanges();

            return true;
        }

        public bool EditBook(BookModel bookin)
        {
            BookKeeperEntities db = new BookKeeperEntities();

            var book = (from b in db.Books
                        where b.Bookid == bookin.Bookid
                        select b).First();

            book.Title = bookin.Title;
            book.Stock = bookin.Stock;
            book.Price = bookin.Price;
            book.Note = bookin.Note;
            book.Description = bookin.Description;

            var bookAuthors = (from ba in db.BookXAuthors
                               join a in db.Authors on ba.Authorid equals a.Authorid
                               where ba.Bookid == book.Bookid
                               select new AuthorModel { Autherid = a.Authorid, Name = a.Name}).ToList();

            var authorsNotInDB = bookAuthors.Except(bookin.Authors);
            var authorsNotInList = bookin.Authors.Except(bookAuthors);

            foreach (AuthorModel author in authorsNotInDB)
            {
                var authordb = db.Authors.Add(new Author { Hidden = false, Name = author.Name });
                db.BookXAuthors.Add(new BookXAuthor { Bookid = book.Bookid, Authorid = authordb.Authorid });
            }

            foreach (var bookAuthor in authorsNotInList)
            {
                var author = db.Authors.Where(x => x.Authorid == bookAuthor.Autherid).First();
                var bookauthor = db.BookXAuthors.Where(x => x.Authorid == author.Authorid).First();

                author.Hidden = true;
                bookauthor.Hidden = true;
            }

            db.SaveChanges();

            return true;
        }
    }
}
