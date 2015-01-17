using Book_Keeper.BookKeeperSR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Keeper.Classes
{
    class BookHandler
    {
        private BookKeeperSR.Service1Client wcfService;
        private BookModel[] books;
        private AuthorModel[] authors;

        public BookHandler()
        {
            wcfService = new BookKeeperSR.Service1Client();
            authors = wcfService.getAuthors().ToArray();
            books = wcfService.GetBooks().ToArray();

        }

        public BookModel getBookByID(int id)
        {
            foreach(BookModel book in books)
            {
                if (book.Bookid == id)
                {
                    return book;
                }
            }

            return null;
        }

        public int getTotalStock()
        {
            int stock = 0;

            foreach (BookModel book in books)
            {
                stock += book.Stock;
            }

            return stock;
        }

        public decimal getTotalStockPrice()
        {
            decimal price = 0;

            foreach (BookModel book in books)
            {
                price += (book.Stock * book.Price);
            }

            return price;
        }

        public bool AddBook(string authorName, string bookTitle, string pricein, string stockin)
        {
            int stock = Convert.ToInt32(stockin);
            decimal price = decimal.Parse(pricein, NumberStyles.Currency);

            List<AuthorModel> authors = new List<AuthorModel>();
            authors.Add(new AuthorModel { Name = authorName});
            BookModel book = new BookModel();
            book.Authors = authors.ToArray();
            book.Title = bookTitle;
            book.Stock = stock;
            book.Price = price;


            wcfService.AddBook(book);

            return true;
        }
    }
}
