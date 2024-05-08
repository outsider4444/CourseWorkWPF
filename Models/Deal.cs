using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkWPF.Models
{
    public class Deal
    {
        public Deal()
        {
            //this.DealProducts = new HashSet<DealProduct>();
        }

        public int Id { get; set; }
        public string Date { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
