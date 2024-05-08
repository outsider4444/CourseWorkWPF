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

namespace CourseWorkWPF.Customers
{
    /// <summary>
    /// Логика взаимодействия для UpdateCustomerWindow.xaml
    /// </summary>
    public partial class UpdateCustomerWindow : Window
    {
        Customer customer_to_update;
        public UpdateCustomerWindow(Customer customer)
        {
            InitializeComponent();

            ContactUpdateInput.Text = customer.Contact;
            PhoneUpdateInput.Text = customer.Phone;
            AddressUpdateInput.Text = customer.Address;
            customer_to_update = customer;
        }

        private void UpdateCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            CustomerViewModel DataContext = new CustomerViewModel();

            if (string.IsNullOrEmpty(ContactUpdateInput.Text) || string.IsNullOrEmpty(PhoneUpdateInput.Text) || string.IsNullOrEmpty(AddressUpdateInput.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            customer_to_update.Contact = ContactUpdateInput.Text;
            customer_to_update.Phone = PhoneUpdateInput.Text;
            customer_to_update.Address = AddressUpdateInput.Text;

            // Удаляем запись из базы данных
            DataContext.UpdateCustomer(customer_to_update);
            MessageBox.Show("Клиент успешно обновлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            // Открываем окно со списком клиентов после успешного добавления
            CustomersWindow customersWindow = new CustomersWindow();
            customersWindow.Show();

            // Закрываем текущее окно добавления
            this.Close();
        }
    }
}
