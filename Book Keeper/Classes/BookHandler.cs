using Book_Keeper.BookKeeperSR;
using System;
using System.Collections.Generic;
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
    }
}
