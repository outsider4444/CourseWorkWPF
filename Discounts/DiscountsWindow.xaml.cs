using CourseWorkWPF.Customers;
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

namespace CourseWorkWPF.Discounts
{
    /// <summary>
    /// Логика взаимодействия для DiscountsWindow.xaml
    /// </summary>
    public partial class DiscountsWindow : Window
    {
        public DiscountsWindow()
        {
            InitializeComponent();

            // Создание экземпляра ViewModel и установка его в контекст данных формы
            DiscountViewModel DataContext = new DiscountViewModel();
            DiscountGrid.ItemsSource = DataContext.Discounts;
        }

        private void CreateDiscountToPage_Click(object sender, RoutedEventArgs e)
        {
            CreateDiscountWindow createDiscountWindow = new CreateDiscountWindow();
            createDiscountWindow.Show();

            this.Close();
        }

        private void DiscountGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCustomer = (Discount)DiscountGrid.SelectedItem;
            if (selectedCustomer != null)
            {
                UpdateDiscountBtn.IsEnabled = true;
                DeleteDiscountBtn.IsEnabled = true;
            }
            else
            {
                UpdateDiscountBtn.IsEnabled = false;
                DeleteDiscountBtn.IsEnabled = false;
            }
        }

        private void DeleteDiscountBtn_Click(object sender, RoutedEventArgs e)
        {
            DiscountViewModel DataContext = new DiscountViewModel();
            // Получаем выбранную запись из DataGrid
            var selectedDiscount = (Discount)DiscountGrid.SelectedItem;

            if (selectedDiscount != null)
            {
                // Удаляем запись из базы данных
                DataContext.DeleteDiscount(selectedDiscount);
                MessageBox.Show("Скидка успешно удалена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                DataContext.LoadData();
                DiscountGrid.ItemsSource = DataContext.Discounts;
            }
        }

        private void UpdateDiscountBtn_Click(object sender, RoutedEventArgs e)
        {
            Discount selectedDiscount = (Discount)DiscountGrid.SelectedItem;

            if (selectedDiscount != null)
            {
                // Удаляем запись из базы данных
                UpdateDiscountWindow updateDiscountWindow = new UpdateDiscountWindow(selectedDiscount);
                updateDiscountWindow.Show();
                this.Close();
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
