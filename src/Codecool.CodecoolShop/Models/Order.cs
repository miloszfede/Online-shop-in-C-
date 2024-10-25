using System;
using System.Collections.Generic;
using System.Linq;

namespace Codecool.CodecoolShop.Models
{
    public class Order : BaseModel
    {
        public List<LineItem> LineItems { get; set; } = new List<LineItem>();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public decimal TotalPrice => LineItems.Sum(item => item.TotalPrice);

        public string _Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        // Billing Address
        public string BillingCountry { get; set; }
        public string BillingCity { get; set; }
        public string BillingZipCode { get; set; }
        public string BillingAddress { get; set; }

        // Shipping Address
        public string ShippingCountry { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingZipCode { get; set; }
        public string ShippingAddress { get; set; }
        }
}