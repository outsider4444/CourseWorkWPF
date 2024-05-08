using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkWPF.Models
{
    public class Discount
    {
        public int Id { get; set; }
        public int MinCount { get; set; }
        public int MaxCount { get; set; }
        public int DiscountCount { get; set; }
        public int DiscountCategoryId { get; set; }

        public virtual DiscountCategory DiscountCategory { get; set; }
    }
}
