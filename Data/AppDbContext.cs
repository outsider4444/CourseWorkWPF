using CourseWorkWPF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkWPF.Data
{
    // 2. Создание контекста данных
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Deal> Deals { get; set; }
        public DbSet<DealProduct> DealProducts { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Models.DiscountCategory> DiscountCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=Ermakov_VT_31;Trusted_Connection=True;TrustServerCertificate=true;");
        }
    }
}
