using System.Linq;
using Codecool.CodecoolShop.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Codecool.CodecoolShop.Services
{
    public class CartService
    {
        private readonly ProductService _productService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ProductService productService;
        private const string CartSessionKey = "Cart";

        public CartService(ProductService productService, IHttpContextAccessor httpContextAccessor)
        {
            _productService = productService;
            _httpContextAccessor = httpContextAccessor;
        }

        public CartService(ProductService productService)
        {
            this.productService = productService;
        }

        private Order GetCartFromSession()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            string cartJson = session.GetString(CartSessionKey);
            return string.IsNullOrEmpty(cartJson) ? new Order() : JsonSerializer.Deserialize<Order>(cartJson);
        }

        public void SaveCartToSession(Order cart)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            string cartJson = JsonSerializer.Serialize(cart);
            session.SetString(CartSessionKey, cartJson);
        }

        public void AddToCart(int productId, int quantity = 1)
        {
            var cart = GetCartFromSession();
            var product = _productService.GetProductById(productId);
            if (product == null) return;

            var existingItem = cart.LineItems.FirstOrDefault(item => item.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.LineItems.Add(new LineItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Quantity = quantity,
                    UnitPrice = product.DefaultPrice
                });
            }

            SaveCartToSession(cart);
        }

        public int GetTotalItemsCount()
        {
            var cart = GetCartFromSession();
            return cart.LineItems.Sum(item => item.Quantity);
        }

        public Order GetCurrentOrder()
        {
            return GetCartFromSession();
        }

        public void UpdateQuantity(int productId, int quantity)
        {
            var cart = GetCartFromSession();
            var existingItem = cart.LineItems.FirstOrDefault(item => item.ProductId == productId);
            
            if (existingItem != null)
            {
                existingItem.Quantity = quantity;
                SaveCartToSession(cart);
            }
        }

        public void RemoveFromCart(int productId)
        {
            var cart = GetCartFromSession();
            var itemToRemove = cart.LineItems.FirstOrDefault(item => item.ProductId == productId);
            
            if (itemToRemove != null)
            {
                cart.LineItems.Remove(itemToRemove);
                SaveCartToSession(cart);
            }
        }

    }
}