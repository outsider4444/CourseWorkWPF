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
using System.Windows.Controls;

namespace CourseWorkWPF.Repository
{
    public class DealViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Deal> _deals;
        public ObservableCollection<Deal> Deals
        {
            get { return _deals; }
            set
            {
                _deals = value;
                OnPropertyChanged(nameof(Deals));
            }
        }

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

        public DealViewModel()
        {
            LoadData();
        }

        public Deal GetDealById(int id)
        {
            return Deals.FirstOrDefault(deal => deal.Id == id);
        }

        public void LoadData()
        {
            using (var dbContext = new AppDbContext())
            {
                Deals = new ObservableCollection<Deal>(dbContext.Deals.ToList());
            }
        }

        public void DeleteDeal(Deal deal)
        {
            using (var dbContext = new AppDbContext())
            {
                dbContext.Deals.Remove(deal);
                dbContext.SaveChanges();
                Deals.Remove(deal); // Обновляем отображаемую коллекцию
            }
        }

        public void UpdateDeal(Deal deal)
        {
            using (var dbContext = new AppDbContext())
            {
                dbContext.Deals.Update(deal);
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
