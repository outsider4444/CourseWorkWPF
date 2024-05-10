using CourseWorkWPF.Data;
using CourseWorkWPF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CourseWorkWPF.Repository
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        public ProductViewModel()
        {
            LoadData();
        }

        public void LoadData()
        {
            using (var dbContext = new AppDbContext())
            {
                Products = new ObservableCollection<Product>(dbContext.Products.ToList());
                List<Models.DiscountCategory> discountCategories = new List<Models.DiscountCategory>(dbContext.DiscountCategories.ToList());
                for (int i = 0; i < Products.Count; i++)
                {
                    for (int j = 0; j < discountCategories.Count; j++)
                    {
                        if (Products[i].DiscountCategoryId == discountCategories[j].Id)
                        {
                            Products[i].DiscountCategory = discountCategories[j];
                            continue;
                        }
                    }
                }
            }
        }

        public Product GetProductById(int id)
        {
            return Products.FirstOrDefault(product => product.Id == id);
        }

        public void DeleteProduct(Product product)
        {
            using (var dbContext = new AppDbContext())
            {
                dbContext.Products.Remove(product);
                dbContext.SaveChanges();
                Products.Remove(product); // Обновляем отображаемую коллекцию
            }
        }

        public void UpdateProduct(Product product)
        {
            using (var dbContext = new AppDbContext())
            {
                dbContext.Products.Update(product);
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
