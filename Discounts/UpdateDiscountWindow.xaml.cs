using CourseWorkWPF.Models;
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
    /// Логика взаимодействия для UpdateDiscountWindow.xaml
    /// </summary>
    public partial class UpdateDiscountWindow : Window
    {
        Discount updatebleDiscount;
        List<DiscountCategory> discountCategories;

        public UpdateDiscountWindow(Discount discount)
        {
            InitializeComponent();

            updatebleDiscount = discount;
        }
    }
}
