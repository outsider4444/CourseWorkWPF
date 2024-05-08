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
    class DiscountCategoryViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<DiscountCategory> _discountCategories;
        public ObservableCollection<DiscountCategory> DiscountCategories
        {
            get { return _discountCategories; }
            set
            {
                _discountCategories = value;
                OnPropertyChanged(nameof(Customers));
            }
        }

        public DiscountCategoryViewModel()
        {
            LoadData();
        }

        public void LoadData()
        {
            using (var dbContext = new AppDbContext())
            {
                DiscountCategories = new ObservableCollection<DiscountCategory>(dbContext.DiscountCategories.ToList());
            }
        }

        public DiscountCategory GetDiscountCategoryById(int id)
        {
            return DiscountCategories.FirstOrDefault(category => category.Id == id);
        }

        public void DeleteDiscountCategory(DiscountCategory discountCategory)
        {
            using (var dbContext = new AppDbContext())
            {
                dbContext.DiscountCategories.Remove(discountCategory);
                dbContext.SaveChanges();
            }
        }

        public void UpdateDiscountCategory(DiscountCategory discountCategory)
        {
            using (var dbContext = new AppDbContext())
            {
                dbContext.DiscountCategories.Update(discountCategory);
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
