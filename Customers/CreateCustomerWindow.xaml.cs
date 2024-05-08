using CourseWorkWPF.Data;
using CourseWorkWPF.Models;
using CourseWorkWPF.Repository;
using System.Windows;

namespace CourseWorkWPF
{
    /// <summary>
    /// Логика взаимодействия для CreateCustomerWindow.xaml
    /// </summary>
    public partial class CreateCustomerWindow : Window
    {
        public CreateCustomerWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string contact = ContactCreateInput.Text;
            string phone = PhoneCreateInput.Text;
            string address = AddressCreateInput.Text;

            // Проверяем, что все поля заполнены
            if (string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(contact) || string.IsNullOrEmpty(address))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // Создаем новый объект Customer
                Customer newCustomer = new Customer
                {
                    Phone = phone,
                    Contact = contact,
                    Address = address
                    // Добавьте другие свойства, если необходимо
                };

                // Добавляем нового клиента в базу данных
                using (var dbContext = new AppDbContext())
                {
                    dbContext.Customers.Add(newCustomer);
                    dbContext.SaveChanges();

                    CustomerViewModel DataContext = new CustomerViewModel();
                    DataContext.LoadData();
                }

                // Очищаем текстовые поля после успешного добавления
                ClearFields();

                MessageBox.Show("Новый клиент успешно добавлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                // Открываем окно со списком клиентов после успешного добавления
                CustomersWindow customersWindow = new CustomersWindow();
                customersWindow.Show();

                // Закрываем текущее окно добавления
                this.Close();
        }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при добавлении клиента: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
}

        private void ClearFields()
        {
            ContactCreateInput.Text = "";
            PhoneCreateInput.Text = "";
            AddressCreateInput.Text = "";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Открываем окно со списком клиентов после успешного добавления
            CustomersWindow customersWindow = new CustomersWindow();
            customersWindow.Show();

            // Закрываем текущее окно добавления
            this.Close();
        }
    }
}
