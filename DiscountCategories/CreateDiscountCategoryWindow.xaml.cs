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

namespace CourseWorkWPF.DiscountCategories
{
    /// <summary>
    /// Логика взаимодействия для CreateDiscountCategoryWindow.xaml
    /// </summary>
    public partial class CreateDiscountCategoryWindow : Window
    {
        public CreateDiscountCategoryWindow()
        {
            InitializeComponent();
        }

        private void SaveNewCategory_Click(object sender, RoutedEventArgs e)
        {
            string name = NameCategoryCreateInput.Text;
            string minprice = MinPriceCategoryCreateInput.Text;
            string maxprice = MaxPriceCategoryCreateInput.Text;

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

            DiscountCategory category = new DiscountCategory
            {
                Name = name,
                MinTotalPrice = minPrice,
                MaxTotalPrice = maxPrice
            };

            // Добавляем новой категории в базу данных
            using (var dbContext = new AppDbContext())
            {
                dbContext.DiscountCategories.Add(category);
                dbContext.SaveChanges();

                DiscountCategoryViewModel DataContext = new DiscountCategoryViewModel();
                DataContext.LoadData();
            }

            // Очищаем текстовые поля после успешного добавления
            ClearFields();

            MessageBox.Show("Новая категория скидки успешно добавлена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            // Открываем окно со списком клиентов после успешного добавления
            DiscountCategoryWindow categoryWindow = new DiscountCategoryWindow();
            categoryWindow.Show();

            // Закрываем текущее окно добавления
            this.Close();
        }
        private void ClearFields()
        {
            NameCategoryCreateInput.Text = "";
            MinPriceCategoryCreateInput.Text = "";
            MaxPriceCategoryCreateInput.Text = "";
        }

    }
}
