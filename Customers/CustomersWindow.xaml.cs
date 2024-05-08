using CourseWorkWPF.Customers;
using CourseWorkWPF.Data;
using CourseWorkWPF.Models;
using CourseWorkWPF.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Логика взаимодействия для CustomersWindow.xaml
    /// </summary>

    public partial class CustomersWindow : Window
    {
        public CustomersWindow()
        {
            InitializeComponent();

            // Создание экземпляра ViewModel и установка его в контекст данных формы
            CustomerViewModel DataContext = new CustomerViewModel();
            CustomerGrid.ItemsSource = DataContext.Customers;
        }

        // Метод удаления из БД
        private void DeleteCustomer_Button_Click(object sender, RoutedEventArgs e)
        {
            CustomerViewModel DataContext = new CustomerViewModel();
            // Получаем выбранную запись из DataGrid
            var selectedCustomer = (Customer)CustomerGrid.SelectedItem;

            if (selectedCustomer != null)
            {
                // Удаляем запись из базы данных
                DataContext.DeleteCustomer(selectedCustomer);
                MessageBox.Show("Клиент успешно удален.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                DataContext.LoadData();
                CustomerGrid.ItemsSource = DataContext.Customers;
            }
        }

        private void CreateCustomer_Button_Click(object sender, RoutedEventArgs e)
        {
            CreateCustomerWindow createCustomerWindow = new CreateCustomerWindow();
            createCustomerWindow.Show();

            this.Close();
        }

        private void UpdateCustomer_Button_Click(object sender, RoutedEventArgs e)
        {
            Customer selectedCustomer = (Customer)CustomerGrid.SelectedItem;

            if (selectedCustomer != null)
            {
                // Удаляем запись из базы данных
                UpdateCustomerWindow updateCustomerWindow = new UpdateCustomerWindow(selectedCustomer);
                updateCustomerWindow.Show();
                this.Close();
            }
        }

        private void CustomerGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCustomer = (Customer)CustomerGrid.SelectedItem;
            if (selectedCustomer != null)
            {
                UpdateCustomerBtn.IsEnabled = true;
                DeleteCustomerBtn.IsEnabled = true;
            }
            else
            {
                UpdateCustomerBtn.IsEnabled = false;
                DeleteCustomerBtn.IsEnabled = false;
            }
        }
    }
}
