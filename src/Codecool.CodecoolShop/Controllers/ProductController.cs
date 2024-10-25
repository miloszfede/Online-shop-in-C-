using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using Codecool.CodecoolShop.Daos.Implementations;

namespace Codecool.CodecoolShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ProductService _productService;
        private readonly CartService _cartService;

        public ProductController(ILogger<ProductController> logger, ProductService productService, CartService cartService)
        {
            _logger = logger;
            _productService = productService;
            _cartService = cartService;
        }

        public IActionResult Index(int? categoryId = null, int? supplierId = null)
        {
            var products = _productService.GetAllProducts().ToList();

            if (categoryId.HasValue)
            {
                products = _productService.GetProductsForCategory(categoryId.Value).ToList();
            }
            else if (supplierId.HasValue)
            {
                products = _productService.GetProductsForSupplier(supplierId.Value).ToList();
            }

            var categories = _productService.GetAllCategories().ToList();
            var suppliers = _productService.GetAllSuppliers().ToList();

            var viewModel = new ProductListViewModel
            {
                Products = products,
                Categories = categories,
                Suppliers = suppliers,
                SelectedCategoryId = categoryId,
                SelectedSupplierId = supplierId
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            _cartService.AddToCart(productId);
            return Json(new { success = true, totalItems = _cartService.GetTotalItemsCount() });
        }
    }
}