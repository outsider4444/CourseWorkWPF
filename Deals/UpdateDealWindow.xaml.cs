using CourseWorkWPF.Data;
using CourseWorkWPF.Models;
using CourseWorkWPF.Repository;
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

namespace CourseWorkWPF.Deals
{
    /// <summary>
    /// Логика взаимодействия для UpdateDealWindow.xaml
    /// </summary>
    public partial class UpdateDealWindow : Window
    {
        List<Customer> customers;
        List<Product> products;
        Deal updateDeal;
        DealProduct updateDealProduct;

        DealViewModel dealViewModel = new DealViewModel();
        DealProductsViewModel dealProductViewModel = new DealProductsViewModel();
        ProductViewModel productViewModel = new ProductViewModel();
        CustomerViewModel customerViewModel = new CustomerViewModel();

        public UpdateDealWindow(Deal deal, DealProduct dealProduct, Customer customer, Product product)
        {
            InitializeComponent();

            customers = customerViewModel.Customers.ToList(); // Получение списка клиентов из базы данных или другого источника
            CustomerUpdateComboBox.ItemsSource = customers;


            products = productViewModel.Products.ToList(); // Получение списка клиентов из базы данных или другого источника
            ProductUpdateComboBox.ItemsSource = products;


            updateDeal = deal;
            updateDealProduct = dealProduct;
            //CountUpdateInput.Text = dealProduct.Count.ToString();
            CountUpdateInput.Text = dealProduct.Count.ToString();
            DateUpdateInput.Text = deal.Date;
            for (int i = 0; i < customers.Count; i++)
            {
                if (customers[i].Id == customer.Id)
                {
                    CustomerUpdateComboBox.SelectedIndex = i;
                    break;
                }
            }
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].Id == product.Id)
                {
                    ProductUpdateComboBox.SelectedIndex = i;
                    break;
                }
            }
        }

        private void UpdateDealButton_Click(object sender, RoutedEventArgs e)
        {

            Customer selected_customer = (Customer)CustomerUpdateComboBox.SelectedItem;
            Product selected_product = (Product)ProductUpdateComboBox.SelectedItem;
            int customer_id = selected_customer.Id;
            int product_id = selected_product.Id;

            string date = DateUpdateInput.Text;
            int count = Int32.Parse(CountUpdateInput.Text);

            if (string.IsNullOrEmpty(date) || CustomerUpdateComboBox.SelectedItem == null || string.IsNullOrEmpty(CountUpdateInput.Text) || ProductUpdateComboBox.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                Product product = productViewModel.GetProductById(product_id);
                Customer customer = customerViewModel.GetCustomerById(customer_id);

                Deal deal = dealViewModel.GetDealById(updateDeal.Id);
                deal.CustomerId = customer.Id;
                deal.Date = date;

                DealProduct dealProduct = dealProductViewModel.GetDealProductByDeal(deal.Id);
                dealProduct.ProductId = product.Id;
                dealProduct.Count = count;

                dealProductViewModel.UpdateDeal(dealProduct);
                dealViewModel.UpdateDeal(deal);

                // Очищаем текстовые поля после успешного добавления
                ClearFields();

                MessageBox.Show("Новая сделка успешно обновлена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                // Открываем окно со списком клиентов после успешного добавления
                DealWindow dealsWindow = new DealWindow(); // Предположим, что ваше главное окно называется MainWindow
                dealsWindow.Show();

                // Закрываем текущее окно добавления
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при обновлении сделки: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearFields()
        {
            DateUpdateInput.Text = "";
            CountUpdateInput.Text = "";
        }
    }
}
