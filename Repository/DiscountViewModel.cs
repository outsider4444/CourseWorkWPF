using CourseWorkWPF.Data;
using CourseWorkWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkWPF.Repository
{
    public class DiscountViewModel: INotifyPropertyChanged
    {
        private ObservableCollection<Discount> _discounts;
        public ObservableCollection<Discount> Discounts
        {
            get { return _discounts; }
            set
            {
                _discounts = value;
                OnPropertyChanged(nameof(Discounts));
            }
        }

        public Discount GetDiscountById(int id)
        {
            return Discounts.FirstOrDefault(discount => discount.Id == id);
        }

        public DiscountViewModel()
        {
            LoadData();
        }

        public void LoadData()
        {
            using (var dbContext = new AppDbContext())
            {
                Discounts = new ObservableCollection<Discount>(dbContext.Discounts.ToList());
            }
        }

        public void DeleteDiscount(Discount discount)
        {
            using (var dbContext = new AppDbContext())
            {
                dbContext.Discounts.Remove(discount);
                dbContext.SaveChanges();
                Discounts.Remove(discount); // Обновляем отображаемую коллекцию
            }
        }

        public void UpdateDiscount(Discount discount)
        {
            using (var dbContext = new AppDbContext())
            {
                dbContext.Discounts.Update(discount);
                dbContext.SaveChanges();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
