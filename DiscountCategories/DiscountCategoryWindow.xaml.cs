using CourseWorkWPF.Models;
using CourseWorkWPF.Products;
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

namespace CourseWorkWPF.DiscountCategories
{
    /// <summary>
    /// Логика взаимодействия для DiscountCategoryWindow.xaml
    /// </summary>
    public partial class DiscountCategoryWindow : Window
    {
        public DiscountCategoryWindow()
        {
            InitializeComponent();

            // Создание экземпляра ViewModel и установка его в контекст данных формы
            DiscountCategoryViewModel DataContext = new DiscountCategoryViewModel();
            CategoryGrid.ItemsSource = DataContext.DiscountCategories;
        }

        private void CategoryGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCategory = (DiscountCategory)CategoryGrid.SelectedItem;
            if (selectedCategory != null)
            {
                UpdateCategoryBtn.IsEnabled = true;
                DeleteCategoryBtn.IsEnabled = true;
            }
            else
            {
                UpdateCategoryBtn.IsEnabled = false;
                DeleteCategoryBtn.IsEnabled = false;
            }
        }

        private void CreateCategoryToPage_Click(object sender, RoutedEventArgs e)
        {
            CreateDiscountCategoryWindow createCategoryWindow = new CreateDiscountCategoryWindow();
            createCategoryWindow.Show();
            this.Close();
        }

        private void DeleteCategoryBtn_Click(object sender, RoutedEventArgs e)
        {
            DiscountCategoryViewModel DataContext = new DiscountCategoryViewModel();
            // Получаем выбранную запись из DataGrid
            var selectedCategory = (DiscountCategory)CategoryGrid.SelectedItem;

            if (selectedCategory != null)
            {
                // Удаляем запись из базы данных
                DataContext.DeleteDiscountCategory(selectedCategory);
                MessageBox.Show("Категория успешно удалена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                DataContext.LoadData();
                CategoryGrid.ItemsSource = DataContext.DiscountCategories;
            }
        }

        private void UpdateCategoryBtn_Click(object sender, RoutedEventArgs e)
        {
            DiscountCategoryViewModel DataContext = new DiscountCategoryViewModel();
            // Получаем выбранную запись из DataGrid
            var selectedCategory = (DiscountCategory)CategoryGrid.SelectedItem;

            if (selectedCategory != null)
            {
                UpdateDiscountCategoryWindow updateCategoryWindow = new UpdateDiscountCategoryWindow(selectedCategory);
                updateCategoryWindow.Show();
                this.Close();
            }
        }
    }
}
