using CourseWorkWPF.Deals;
using CourseWorkWPF.Models;
using CourseWorkWPF.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
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

namespace CourseWorkWPF
{
    /// <summary>
    /// Логика взаимодействия для DealWindow.xaml
    /// </summary>
    /// 

    public class ProductsListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<Product> products)
            {
                // Преобразование списка адресов в одну строку
                return string.Join(", ", products.Select(a => $"{a.Id}, {a.Name}"));
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DealsListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Deal deals)
            {
                // Преобразование списка адресов в одну строку
                return deals.Id;
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public partial class DealWindow : Window
    {
        public DealWindow()
        {
            InitializeComponent();

            // Создание экземпляра ViewModel и установка его в контекст данных формы
            DealProductsViewModel DataContext = new DealProductsViewModel();
            DealGrid.ItemsSource = DataContext.DealProducts;
        }

        private void CreateDealToPage_Click(object sender, RoutedEventArgs e)
        {
            CreateDealWindow createDealWindow = new CreateDealWindow();
            createDealWindow.Show();

            this.Close();
        }


        private void DeleteDealBtn_Click(object sender, RoutedEventArgs e)
        {
            DealProductsViewModel DataContext = new DealProductsViewModel();
            // Получаем выбранную запись из DataGrid
            var selectedDeal = (DealProduct)DealGrid.SelectedItem;

            if (selectedDeal != null)
            {
                // Удаляем запись из базы данных
                DataContext.DeleteDealProduct(selectedDeal);
                MessageBox.Show("Сделка успешно удалена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                DataContext.LoadData();
                DealGrid.ItemsSource = DataContext.DealProducts;
            }
        }

        private void UpdateDealBtn_Click(object sender, RoutedEventArgs e)
        {
            DealProductsViewModel dealProductsViewModel = new DealProductsViewModel();
            DealViewModel dealViewModel = new DealViewModel();
            CustomerViewModel customerViewModel = new CustomerViewModel();
            // Получаем выбранную запись из DataGrid

            var selectedDealProduct = DealGrid.SelectedItem as DealProduct;
            var selectedDeal = DealGrid.SelectedItem as Deal;

            if (selectedDealProduct != null)
            {
                // Удаляем запись из базы данных

                Deal deal = selectedDealProduct.Deal;
                Customer customer = customerViewModel.GetCustomerById(deal.CustomerId);
                Product product = selectedDealProduct.Product;


                UpdateDealWindow updateDealWindow = new UpdateDealWindow(deal, selectedDealProduct, customer, product);
                updateDealWindow.Show();

            }
            else if(selectedDeal != null)
            {
                Customer customer = customerViewModel.GetCustomerById(selectedDeal.CustomerId);
                DealProduct dealProduct = dealProductsViewModel.GetDealProductByDeal(selectedDeal.Id);
                Product product = dealProduct.Product;

                UpdateDealWindow updateDealWindow = new UpdateDealWindow(selectedDeal, dealProduct, customer, product);
                updateDealWindow.Show();

            }
            this.Close();
        }

        private void DealGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedDeal = DealGrid.SelectedItem;
            if (selectedDeal != null)
            {
                UpdateDealBtn.IsEnabled = true;
                DeleteDealBtn.IsEnabled = true;
            }
            else
            {
                UpdateDealBtn.IsEnabled = false;
                DeleteDealBtn.IsEnabled = false;
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
