namespace CourseWorkWPF.Models
{
    public class Product
    {
        public Product()
        {

            this.DealProducts = new HashSet<DealProduct>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int WPrice { get; set; }
        public int RPrice { get; set; }
        public string Description { get; set; }
        public int DiscountCategoryId { get; set; }
        public virtual DiscountCategory DiscountCategory { get; set; }
        public virtual ICollection<DealProduct> DealProducts { get; set; }

    }
}
