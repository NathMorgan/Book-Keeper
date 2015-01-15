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
                bookModels.Add(new BookModel { Authors = authors, Bookid = book.Bookid, Price = book.Price, Stock = book.Stock, Title = book.Title });
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

            return new BookModel { Authors = authors, Bookid = book.Bookid, Price = book.Price, Stock = book.Stock, Title = book.Title };
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
        public bool AddBook(string book)
        {
            return true;
        }

        /**
         * Adds an author to the database
         *
         * @return bool
         */
        public bool AddAuthor(string author)
        {
            return true;
        }
    }
}
