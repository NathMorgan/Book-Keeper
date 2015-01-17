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
            //Getting current button clicked
            string buttonName = (sender as Button).Content.ToString();

            //Outputting to console that the button was clicked
            Console.WriteLine(DateTime.Now.ToString("h:mm:ss") + ": Button clicked: " + buttonName);

            //Checking what button was clicked and displaying details to logbox
            switch (buttonName)
            {
                case "Add Record":
                    AddRecordButton();
                    break;
                case "Display Count":
                    LogBox.Text += "\n " + "Total Stock Count: " + bookhandle.getTotalStock();
                    break;
                case "Display Stock Value":
                    LogBox.Text += "\n " + "Total Stock Price: £" + bookhandle.getTotalStockPrice();
                    break;
            }
        }

        private void AddRecordButton()
        {
            //Getting the values from the input boxes
            string authorName = Author_Name.Text;
            string bookTitle = Book_Title.Text;
            string price = Price.Text;
            string stock = Stock_Quantity.Text;

            try
            {
                if (bookhandle.AddBook(authorName, bookTitle, price, stock))
                    LogBox.Text += "\n " + "Book Added!";
            }
            catch (Exception e)
            {
                Console.WriteLine("Error adding record: " + e.Message);
            }

        }
    }
}
