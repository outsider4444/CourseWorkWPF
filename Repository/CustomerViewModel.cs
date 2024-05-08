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

namespace CourseWorkWPF.Repository
{
    public class CustomerViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Customer> _customers;
        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
            set
            {
                _customers = value;
                OnPropertyChanged(nameof(Customers));
            }
        }

        public Customer GetCustomerById(int id)
        {
            return Customers.FirstOrDefault(customer => customer.Id == id);
        }

        public CustomerViewModel()
        {
            LoadData();
        }

        public void LoadData()
        {
            using (var dbContext = new AppDbContext())
            {
                Customers = new ObservableCollection<Customer>(dbContext.Customers.ToList());
            }
        }

        public void DeleteCustomer(Customer customer)
        {
            using (var dbContext = new AppDbContext())
            {
                dbContext.Customers.Remove(customer);
                dbContext.SaveChanges();
                Customers.Remove(customer); // Обновляем отображаемую коллекцию
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            using (var dbContext = new AppDbContext())
            {
                dbContext.Customers.Update(customer);
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
