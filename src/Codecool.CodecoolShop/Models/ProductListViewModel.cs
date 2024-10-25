using System.Collections.Generic;

namespace Codecool.CodecoolShop.Models
{
    public class ProductListViewModel
    {
        public List<Product> Products { get; set; }
        public List<ProductCategory> Categories { get; set; }
        public List<Supplier> Suppliers { get; set; }
        public int? SelectedCategoryId { get; set; }
        public int? SelectedSupplierId { get; set; }
    }
}