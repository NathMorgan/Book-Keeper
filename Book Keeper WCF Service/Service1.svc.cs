using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Book_Keeper_WCF_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public IEnumerable<Book> GetBooks()
        {
            BookKeeperEntities db = new BookKeeperEntities();
            var books = db.Books.Where(q => q.Hidden == false);
            return books;
        }

        public IEnumerable<Author> getAuthors()
        {
            BookKeeperEntities db = new BookKeeperEntities();
            var authors = db.Authors.Where(q => q.Hidden == false);
            return authors;
        }

        public string GetBooksWithAuthors()
        {
            return "";
        }

        public string GetBookById(int id)
        {
            return "";
        }

        public string GetAuthorById(int id)
        {
            return "";
        }

        public string AddBook(string book)
        {
            return "";
        }

        public string AddAuthor(string author)
        {
            return "";
        }
    }
}
