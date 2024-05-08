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

namespace CourseWorkWPF.Products
{
    /// <summary>
    /// Логика взаимодействия для UpdateProductWindow.xaml
    /// </summary>
    public partial class UpdateProductWindow : Window
    {
        List<DiscountCategory> discountCategories;
        DiscountCategoryViewModel discountCategoryViewModel = new DiscountCategoryViewModel();
        Product updatableProduct;
        public UpdateProductWindow(Product product, DiscountCategory discountCategory)
        {
            InitializeComponent();

            discountCategories = discountCategoryViewModel.DiscountCategories.ToList();
            updatableProduct = product;

            ProductNameUpdateInput.Text = product.Name;
            ProductRPriceUpdateInput.Text = product.RPrice.ToString();
            ProductWPriceUpdateInput.Text = product.WPrice.ToString();
            ProductDescriptionUpdateInput.Text = product.Description;

            ProductDiscountCategoryUpdateComboBox.ItemsSource = discountCategories;
            for (int i = 0; i < discountCategories.Count; i++)
            {
                if (discountCategories[i].Id == discountCategory.Id)
                {
                    ProductDiscountCategoryUpdateComboBox.SelectedItem = discountCategories[i];
                    break;
                }
            }


        }

        private void UpdateProductButton_Click(object sender, RoutedEventArgs e)
        {
            string name = ProductNameUpdateInput.Text;
            int rPrice = Int32.Parse(ProductRPriceUpdateInput.Text);
            int wPrice = Int32.Parse(ProductWPriceUpdateInput.Text);
            string description = ProductDescriptionUpdateInput.Text;
            DiscountCategory? category = ProductDiscountCategoryUpdateComboBox.SelectedItem as DiscountCategory;
            int category_id = category.Id;

            updatableProduct.Name = name;
            updatableProduct.RPrice = rPrice;
            updatableProduct.WPrice = wPrice;
            updatableProduct.Description = description;
            updatableProduct.DiscountCategoryId = category_id;


            // Добавляем нового клиента в базу данных
            using (var dbContext = new AppDbContext())
            {
                dbContext.Products.Update(updatableProduct);
                dbContext.SaveChanges();

                ProductViewModel DataContext = new ProductViewModel();
                DataContext.LoadData();
            }

            MessageBox.Show("Новый товар успешно обновлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            // Открываем окно со списком клиентов после успешного добавления
            ProductsWindow productsWindow = new ProductsWindow();
            productsWindow.Show();

            // Закрываем текущее окно добавления
            this.Close();
        }
    }
}
