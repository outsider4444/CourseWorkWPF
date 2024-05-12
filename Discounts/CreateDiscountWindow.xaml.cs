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

namespace CourseWorkWPF.Discounts
{
    /// <summary>
    /// Логика взаимодействия для CreateDiscountWindow.xaml
    /// </summary>
    public partial class CreateDiscountWindow : Window
    {
        List<DiscountCategory> discountCategories;

        public CreateDiscountWindow()
        {
            InitializeComponent();

            DiscountCategoryViewModel discountCategoryViewModel = new DiscountCategoryViewModel();
            discountCategories = discountCategoryViewModel.DiscountCategories.ToList();
            CategoryCreateComboBox.ItemsSource = discountCategories;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            string StringMinDiscount = DiscountMinCountCreateInput.Text;
            string StringMaxDiscount = DiscountMaxCountCreateInput.Text;
            string StringDiscount = DiscountCountCreateInput.Text;
            var newDiscountCategory = CategoryCreateComboBox.SelectedItem;

            // Проверяем, что все поля заполнены
            if (newDiscountCategory == null || string.IsNullOrEmpty(StringMinDiscount) || string.IsNullOrEmpty(StringMaxDiscount) || string.IsNullOrEmpty(StringDiscount))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int minDiscount = Int32.Parse(StringMinDiscount);
            int maxDiscount = Int32.Parse(StringMaxDiscount);
            int discount = Int32.Parse(StringDiscount);
            if (minDiscount > maxDiscount)
            {
                MessageBox.Show("Значение минимального количества не может быть выше максимального.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DiscountCategory? category = CategoryCreateComboBox.SelectedItem as DiscountCategory;
            int category_id = category.Id;

            Discount newDiscount = new Discount
            {
                MaxCount = maxDiscount,
                MinCount = minDiscount,
                DiscountCategoryId = category_id,
                DiscountCount = discount
            };

            // Добавляем нового продукта в базу данных
            using (var dbContext = new AppDbContext())
            {
                dbContext.Discounts.Add(newDiscount);
                dbContext.SaveChanges();

                DiscountViewModel DataContext = new DiscountViewModel();
                DataContext.LoadData();
            }

            // Очищаем текстовые поля после успешного добавления
            ClearFields();

            MessageBox.Show("Новая скидка успешно добавлена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            // Открываем окно со списком клиентов после успешного добавления
            DiscountsWindow productsWindow = new DiscountsWindow();
            productsWindow.Show();

            // Закрываем текущее окно добавления
            this.Close();
        }

        private void ClearFields()
        {
            DiscountMinCountCreateInput.Text = "";
            DiscountMaxCountCreateInput.Text = "";
            DiscountCountCreateInput.Text = "";
        }
    }
}
