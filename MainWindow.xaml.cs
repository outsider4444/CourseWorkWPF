using CourseWorkWPF.Data;
using CourseWorkWPF.DiscountCategories;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseWorkWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_To_Customers(object sender, RoutedEventArgs e)
        {
            Window mainWindow = Window.GetWindow(this);
            CustomersWindow customers = new CustomersWindow();

            customers.Show();
            this.Close();
        }

        private void Button_Click_To_Deals (object sender, RoutedEventArgs e)
        {
            Window mainWindow = Window.GetWindow(this);
            DealWindow deals = new DealWindow();

            deals.Show();
            this.Close();
        }

        private void Button_Click_To_Products(object sender, RoutedEventArgs e)
        {
            ProductsWindow productsWindow = new ProductsWindow();

            productsWindow.Show();
            this.Close();
        }

        private void DealCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            DiscountCategoryWindow categoryWindow = new DiscountCategoryWindow();

            categoryWindow.Show();
            this.Close();
        }
    }
}