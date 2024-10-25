using Microsoft.AspNetCore.Mvc;
using Codecool.CodecoolShop.Services;
using System.Collections.Generic;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            var cart = _cartService.GetCurrentOrder();
            return View(cart);
        }

        [HttpPost]
        public IActionResult UpdateCart(Dictionary<int, int> quantities, int? removeProductId)
        {
            if (removeProductId.HasValue)
            {
                _cartService.RemoveFromCart(removeProductId.Value);
            }
            else
            {
                foreach (var item in quantities)
                {
                    if (item.Value > 0)
                    {
                        _cartService.UpdateQuantity(item.Key, item.Value);
                    }
                    else
                    {
                        _cartService.RemoveFromCart(item.Key);
                    }
                }
            }

            return RedirectToAction("Index");
        }

         public IActionResult Checkout()
        {
            var cart = _cartService.GetCurrentOrder();
            return View(cart);
        }

        
        [HttpPost]
        public IActionResult SubmitCheckout(Order checkoutOrder)
        {
            if (ModelState.IsValid)
            {
                var cart = _cartService.GetCurrentOrder();

                
                cart.Name = checkoutOrder.Name;
                cart.Email = checkoutOrder.Email;
                cart.PhoneNumber = checkoutOrder.PhoneNumber;
                cart.BillingCountry = checkoutOrder.BillingCountry;
                cart.BillingCity = checkoutOrder.BillingCity;
                cart.BillingZipCode = checkoutOrder.BillingZipCode;
                cart.BillingAddress = checkoutOrder.BillingAddress;
                cart.ShippingCountry = checkoutOrder.ShippingCountry;
                cart.ShippingCity = checkoutOrder.ShippingCity;
                cart.ShippingZipCode = checkoutOrder.ShippingZipCode;
                cart.ShippingAddress = checkoutOrder.ShippingAddress;

               
                _cartService.SaveCartToSession(cart);

                return RedirectToAction("Payment");
            }

            return View("Checkout", checkoutOrder); 
        }

        
        public IActionResult Payment()
        {
            return View(); 
        }

    }
}