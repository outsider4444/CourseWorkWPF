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

namespace CourseWorkWPF
{
    /// <summary>
    /// Логика взаимодействия для ProductsWindow.xaml
    /// </summary>
    public partial class ProductsWindow : Window
    {
        public ProductsWindow()
        {
            InitializeComponent();

            // Создание экземпляра ViewModel и установка его в контекст данных формы
            ProductViewModel DataContext = new ProductViewModel();
            ProductGrid.ItemsSource = DataContext.Products;
        }

        private void ProductGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedProduct = (Product)ProductGrid.SelectedItem;
            if (selectedProduct != null)
            {
                UpdateProductBtn.IsEnabled = true;
                DeleteProductBtn.IsEnabled = true;
            }
            else
            {
                UpdateProductBtn.IsEnabled = false;
                DeleteProductBtn.IsEnabled = false;
            }
        }

        private void CreateProductToPage_Click(object sender, RoutedEventArgs e)
        {
            CreateProductWindow createProductWindow = new CreateProductWindow();
            createProductWindow.Show();
            this.Close();
        }

        private void DeleteProductBtn_Click(object sender, RoutedEventArgs e)
        {
            ProductViewModel DataContext = new ProductViewModel();
            // Получаем выбранную запись из DataGrid
            var selectedProduct = (Product)ProductGrid.SelectedItem;

            if (selectedProduct != null)
            {
                // Удаляем запись из базы данных
                DataContext.DeleteProduct(selectedProduct);
                MessageBox.Show("Товар успешно удален.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                DataContext.LoadData();
                ProductGrid.ItemsSource = DataContext.Products;
            }
        }

        private void UpdateProductBtn_Click(object sender, RoutedEventArgs e)
        {
            ProductViewModel DataContext = new ProductViewModel();
            DiscountCategoryViewModel CategoryDataContext = new DiscountCategoryViewModel();
            // Получаем выбранную запись из DataGrid
            var selectedProduct = (Product)ProductGrid.SelectedItem;

            if (selectedProduct != null)
            {
                DiscountCategory discountCategory = CategoryDataContext.GetDiscountCategoryById(selectedProduct.DiscountCategoryId);
                UpdateProductWindow updateProductWindow = new UpdateProductWindow(selectedProduct, discountCategory);
                updateProductWindow.Show();
                this.Close();
            }
        }
    }
}
