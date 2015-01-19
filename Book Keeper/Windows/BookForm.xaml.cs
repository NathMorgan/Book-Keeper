using Book_Keeper.BookKeeperSR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Book_Keeper.Windows
{
    /// <summary>
    /// Interaction logic for BookForm.xaml
    /// </summary>
    public partial class BookForm : Window
    {
        public BookForm(BookModel book = null)
        {
            InitializeComponent();

            //If book is not null then it will be an edit book form else its a new book form
            if (book != null)
            {
                this.Title = "Edit Book";

                BookTitle.Text = book.Title;
                BookStock.Text = book.Stock.ToString();
                BookPrice.Text = book.Price.ToString();
                BookNote.Text = book.Note;
                BookDescription.Text = book.Description;
                BookAuthors.Text = "";
                foreach (var author in book.Authors)
                {
                    BookAuthors.Text += author.Name + ", ";
                }
                BookButton.Content = "Edit Book";
            }
        }

        private void BookButton_Click(object sender, RoutedEventArgs e)
        {
            //Getting the values from the text box

            var title = BookTitle.Text;
            var stock = BookStock.Text;
            var price = BookPrice.Text;
            var note = BookNote.Text;
            var description = BookDescription.Text;
            var authors = BookAuthors.Text;

            string buttonName = (sender as Button).Content.ToString();

            switch (buttonName)
            {
                case "Edit book":
                    break;
                case "Add Book":
                    break;
            }
        }
    }
}
