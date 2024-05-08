using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkWPF.Models
{
    public partial class DiscountCategory
    {
        public DiscountCategory()
        {
            this.Discounts = new HashSet<Discount>();
            this.Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string MinTotalPrice { get; set; }
        public string MaxTotalPrice { get; set; }

        public virtual ICollection<Discount> Discounts { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
