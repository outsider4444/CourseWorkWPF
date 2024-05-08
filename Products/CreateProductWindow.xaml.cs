using CourseWorkWPF.Data;
using CourseWorkWPF.Models;
using CourseWorkWPF.Repository;
using System;
using System.Collections.Generic;
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

namespace CourseWorkWPF.Products
{
    /// <summary>
    /// Логика взаимодействия для CreateProductWindow.xaml
    /// </summary>
    public partial class CreateProductWindow : Window
    {
        List<DiscountCategory> discountCategories;

        public CreateProductWindow()
        {
            InitializeComponent();

            DiscountCategoryViewModel discountCategoryViewModel = new DiscountCategoryViewModel();
            discountCategories = discountCategoryViewModel.DiscountCategories.ToList();
            ProductDiscountCategoryCreateComboBox.ItemsSource = discountCategories;
            for (int i = 0; i < discountCategories.Count; i++)
            {
                if (discountCategories[i].Name == "Нет")
                {
                    ProductDiscountCategoryCreateComboBox.SelectedItem = discountCategories[i];
                    break;
                }
            }
        }

        private void CreateProductButton_Click(object sender, RoutedEventArgs e)
        {
            string name = ProductNameCreateInput.Text;
            string wprice = ProductWPriceCreateInput.Text;
            string rprice = ProductRPriceCreateInput.Text;
            string description = ProductDescriptionCreateInput.Text;
            DiscountCategory? newDiscountCategory = ProductDiscountCategoryCreateComboBox.SelectedItem as DiscountCategory;
            // Проверяем, что все поля заполнены
            if (newDiscountCategory == null || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(wprice) || string.IsNullOrEmpty(rprice) || string.IsNullOrEmpty(description))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Product product = new Product
            {
                Name = name,
                WPrice = Int32.Parse(wprice),
                RPrice = Int32.Parse(rprice),
                Description = description,
                DiscountCategoryId = newDiscountCategory.Id
            };
            // Добавляем нового клиента в базу данных
            using (var dbContext = new AppDbContext())
            {
                dbContext.Products.Add(product);
                dbContext.SaveChanges();

                ProductViewModel DataContext = new ProductViewModel();
                DataContext.LoadData();
            }
            // Очищаем текстовые поля после успешного добавления
            ClearFields();

            MessageBox.Show("Новый товар успешно добавлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            // Открываем окно со списком клиентов после успешного добавления
            ProductsWindow productsWindow = new ProductsWindow();
            productsWindow.Show();

            // Закрываем текущее окно добавления
            this.Close();
        }

        private void ClearFields()
        {
            ProductNameCreateInput.Text = "";
            ProductWPriceCreateInput.Text = "";
            ProductRPriceCreateInput.Text = "";
            ProductDescriptionCreateInput.Text = "";
        }
    }
}
