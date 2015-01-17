using Book_Keeper.BookKeeperSR;
using Book_Keeper.Classes;
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

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddRecordButton()
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var bookHander = new BookHandler();

            TotalStockVal.Content = bookHander.getTotalStock() + " (£" + bookhandle.getTotalStockPrice() + ")";

            List<BookModel> books = bookHander.getBooks().ToList();

            BookListBox.ItemsSource = books.Select(x => x.Title);


            /*
            System.Windows.Data.CollectionViewSource bookModelViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("bookModelViewSource")));

            //Loading object into the DataView
            var bookHander = new BookHandler();
            bookModelViewSource.Source = bookHander.getBooks();
             * */
        }
    }
}
