using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkWPF.Models
{
    public class DealProduct
    {
        public DealProduct()
        {
            //this.Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public int Count { get; set; }
        public int TotalPrice { get; set; }
        public int DealId { get; set; }
        public int ProductId { get; set; }

        //public virtual ICollection<Product> Products { get; set; }
        public virtual Deal Deal { get; set; }
        public virtual Product Product { get; set; }
    }
}
