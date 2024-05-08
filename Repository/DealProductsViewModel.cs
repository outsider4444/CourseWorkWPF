using CourseWorkWPF.Data;
using CourseWorkWPF.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CourseWorkWPF.Repository
{
    public class DealProductsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<DealProduct> _dealProducts;
        public ObservableCollection<DealProduct> DealProducts
        {
            get { return _dealProducts; }
            set
            {
                _dealProducts = value;
                OnPropertyChanged(nameof(DealProducts));
            }
        }

        public DealProductsViewModel()
        {
            LoadData();
        }

        public void LoadData()
        {
            using (var dbContext = new AppDbContext())
            {
                var query = from dp in dbContext.DealProducts
                            join p in dbContext.Products on dp.ProductId equals p.Id
                            join d in dbContext.Deals on dp.DealId equals d.Id
                            select new
                            {
                                DealProduct = dp,
                                Deal = d,
                                Products = p
                            };
                var dealsWithProductsQuery = query.ToList();
                ObservableCollection<DealProduct> _dealProducts = new ObservableCollection<DealProduct>();

                foreach (var item in dealsWithProductsQuery)
                {
                    DealProduct dealProduct = item.DealProduct;
                    Product product = item.Products;
                    dealProduct.Product=product;

                    //foreach (var prod in item.Products)
                    //{
                    //    dealProduct.Products.Add(prod);
                    //}

                    _dealProducts.Add(dealProduct);
                }
                DealProducts = _dealProducts;

            }
        }

        public DealProduct GetDealProductByDeal(int deal_id)
        {
            return DealProducts.FirstOrDefault(deal => deal.DealId == deal_id);
        }

        public void DeleteDealProduct(DealProduct dealProduct)
        {
            using (var dbContext = new AppDbContext())
            {
                dbContext.DealProducts.Remove(dealProduct);
                dbContext.SaveChanges();
                DealProducts.Remove(dealProduct); // Обновляем отображаемую коллекцию
            }
        }

        public void UpdateDeal(DealProduct deal)
        {
            using (var dbContext = new AppDbContext())
            {
                deal.TotalPrice = deal.Count * deal.Product.WPrice;
                dbContext.DealProducts.Update(deal);
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
