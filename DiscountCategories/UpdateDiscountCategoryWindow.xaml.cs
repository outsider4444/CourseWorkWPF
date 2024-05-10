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

namespace CourseWorkWPF.DiscountCategories
{
    /// <summary>
    /// Логика взаимодействия для UpdateDiscountCategoryWindow.xaml
    /// </summary>
    public partial class UpdateDiscountCategoryWindow : Window
    {
        DiscountCategory updatebleCategory;
        public UpdateDiscountCategoryWindow(DiscountCategory discountCategory)
        {
            InitializeComponent();
            NameCategoryUpdateInput.Text = discountCategory.Name;
            MinPriceCategoryUpdateInput.Text = discountCategory.MinTotalPrice.ToString();
            MaxPriceCategoryUpdateInput.Text = discountCategory.MaxTotalPrice.ToString();
            updatebleCategory = discountCategory;
        }
        private void UpdateCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            DiscountCategoryViewModel DataContext = new DiscountCategoryViewModel();

            string name = NameCategoryUpdateInput.Text;
            string minprice = MinPriceCategoryUpdateInput.Text;
            string maxprice = MaxPriceCategoryUpdateInput.Text;

            // Проверяем, что все поля заполнены
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(minprice) || string.IsNullOrEmpty(maxprice))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int minPrice = Int32.Parse(minprice);
            int maxPrice = Int32.Parse(maxprice);

            if (minPrice > maxPrice)
            {
                MessageBox.Show("Минимальная цена не может быть больше максимальной.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            updatebleCategory.Name = name;
            updatebleCategory.MinTotalPrice = minPrice;
            updatebleCategory.MaxTotalPrice = maxPrice;

            // Удаляем запись из базы данных
            DataContext.UpdateDiscountCategory(updatebleCategory);
            MessageBox.Show("Категория успешно обновлена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            // Очищаем текстовые поля после успешного добавления
            ClearFields();

            // Открываем окно со списком клиентов после успешного добавления
            DiscountCategoryWindow categoryWindow = new DiscountCategoryWindow();
            categoryWindow.Show();

            // Закрываем текущее окно добавления
            this.Close();
        }

        private void ClearFields()
        {
            NameCategoryUpdateInput.Text = "";
            MinPriceCategoryUpdateInput.Text = "";
            MaxPriceCategoryUpdateInput.Text = "";
        }

    }
}
