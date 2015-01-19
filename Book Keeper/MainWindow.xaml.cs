using Book_Keeper.BookKeeperSR;
using Book_Keeper.Classes;
using Book_Keeper.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Book_Keeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BookHandler bookhandle = new BookHandler();
        ObservableCollection<string> bookNameList = new ObservableCollection<string>();

        public MainWindow()
        {
            InitializeComponent();

            TotalStockVal.Content = bookhandle.getTotalStock() + " (£" + bookhandle.getTotalStockPrice() + ")";

            var books = bookhandle.getBooks();
            foreach (var book in books)
            {
                bookNameList.Add(book.Title);
            }
            BookListBox.ItemsSource = bookNameList;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void BookListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(BookListBox.SelectedIndex != -1)
            {
                var selectedBookName = BookListBox.SelectedValue.ToString();

                var bookHander = new BookHandler();

                List<BookModel> books = bookHander.getBooks();

                BookModel selectedBook = books.Where(x => x.Title == selectedBookName).First();

                if (selectedBook != null)
                {
                    AuthorVal.Text = "";

                    foreach (AuthorModel author in selectedBook.Authors)
                    {
                        AuthorVal.Text += author.Name + "\n";
                    }
                    TitleVal.Text = selectedBook.Title;
                    StockVal.Text = selectedBook.Stock.ToString();
                    PriceVal.Text = "£" + selectedBook.Price;

                    if (selectedBook.Description != "")
                        DescriptionVal.Text = selectedBook.Description;
                    if (selectedBook.Note != "")
                        NoteVal.Text = selectedBook.Note;
                }
            }
            else
            {
                TitleVal.Text = "N/A";
                StockVal.Text = "N/A";
                PriceVal.Text = "N/A";
                DescriptionVal.Text = "N/A";
                NoteVal.Text = "N/A";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var bookHander = new BookHandler();

            //Getting current button clicked
            string buttonName = (sender as Button).Content.ToString();

            //Outputting to console that the button was clicked
            Console.WriteLine(DateTime.Now.ToString("h:mm:ss") + ": Button clicked: " + buttonName);

            switch (buttonName)
            {
                case "Delete" :
                    MessageBoxResult deleteResult = MessageBox.Show("Do you wish to delete this book?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (deleteResult == MessageBoxResult.Yes)
                    {
                        bookhandle.DeleteBookByName(BookListBox.SelectedItem.ToString());
                        TotalStockVal.Content = bookHander.getTotalStock() + " (£" + bookhandle.getTotalStockPrice() + ")";

                        bookNameList.RemoveAt(BookListBox.SelectedIndex);

                        BookListBox.ItemsSource = bookNameList;
                    }
                    break;
                case "Edit" :
                    BookForm bookformwin = new BookForm(bookhandle.getBookByName(BookListBox.SelectedItem.ToString()));
                    bookformwin.Show();

                    //Deleting old bookname and readding it due to ObservableCollection not picking up changes
                    var updatedBook = bookHander.getBookByName(BookListBox.SelectedValue.ToString());
                    bookNameList.RemoveAt(BookListBox.SelectedIndex);
                    bookNameList.Add(updatedBook.Title);

                    break;
                case "Add Book" :
                    break;
            }
        }
    }
}
