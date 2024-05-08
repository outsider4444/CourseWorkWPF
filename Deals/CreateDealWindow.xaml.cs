using CourseWorkWPF.Data;
using CourseWorkWPF.Models;
using CourseWorkWPF.Repository;
using Microsoft.IdentityModel.Tokens;
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

namespace CourseWorkWPF
{
    /// <summary>
    /// Логика взаимодействия для CreateDealWindow.xaml
    /// </summary>
    public partial class CreateDealWindow : Window
    {
        ProductViewModel productViewModel = new ProductViewModel();
        CustomerViewModel customerViewModel = new CustomerViewModel();

        public CreateDealWindow()
        {
            InitializeComponent();
            List<Customer> customers = customerViewModel.Customers.ToList(); // Получение списка клиентов из базы данных или другого источника
            CustomerComboBox.ItemsSource = customers;


            List<Product> products = productViewModel.Products.ToList(); // Получение списка клиентов из базы данных или другого источника
            ProductComboBox.ItemsSource = products;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Customer selected_customer = (Customer)CustomerComboBox.SelectedItem;
            Product selected_product = (Product)ProductComboBox.SelectedItem;
            int customer_id = selected_customer.Id;
            int product_id = selected_product.Id;

            string date = DateCreateInput.Text;
            int count = Int32.Parse(CountCreateInput.Text);

            // Проверяем, что все поля заполнены
            if (string.IsNullOrEmpty(date) || CustomerComboBox.SelectedItem == null || string.IsNullOrEmpty(CountCreateInput.Text) || ProductComboBox.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                Product product = productViewModel.GetProductById(product_id);
                Customer customer = customerViewModel.GetCustomerById(customer_id);
                List<Product> newProductList = new List<Product> { product };

                // Создаем новый объект Deal
                Deal newDeal = new Deal
                {
                    CustomerId = customer.Id,
                    Date = date,
                    // Добавьте другие свойства, если необходимо
                };

                using (var dbContext = new AppDbContext())
                {
                    dbContext.Deals.Add(newDeal);
                    dbContext.SaveChanges();
                }

                DealProduct newDealProduct = new DealProduct
                {
                    DealId = newDeal.Id,
                    ProductId = newProductList[0].Id,
                    TotalPrice = product.WPrice * count,
                    Count = count
                };

                // Добавляем нового клиента в базу данных
                using (var dbContext = new AppDbContext())
                {
                    dbContext.DealProducts.Add(newDealProduct);
                    dbContext.SaveChanges();

                    DealViewModel DataContext = new DealViewModel();
                    DataContext.LoadData();
                }

                // Очищаем текстовые поля после успешного добавления
                ClearFields();

                MessageBox.Show("Новая сделка успешно добавлена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                // Открываем окно со списком клиентов после успешного добавления
                DealWindow dealsWindow = new DealWindow(); // Предположим, что ваше главное окно называется MainWindow
                dealsWindow.Show();

                // Закрываем текущее окно добавления
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при добавлении сделки: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearFields()
        {
            DateCreateInput.Text = "";
            CountCreateInput.Text = "";
        }
    }
}
