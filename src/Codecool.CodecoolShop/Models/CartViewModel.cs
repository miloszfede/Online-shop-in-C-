using Codecool.CodecoolShop.Models;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Models
{
    public class CartViewModel
    {
        public List<LineItem> LineItems { get; set; }
        public decimal TotalPrice { get; set; }
    }
}