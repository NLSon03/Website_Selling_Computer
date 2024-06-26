using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Website_Selling_Computer.Models;
using Website_Selling_Computer.Repositories.Interfaces;
namespace Website_Selling_Computer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProduct _productRepo;
        private readonly IProductCategory _productCategoryRepo;
        private readonly IProductDetails _productDetailsRepo;
        private readonly IManufacturer _manufacturerRepository;
        public HomeController(ILogger<HomeController> logger, IProduct productRepo,IProductCategory productCategory,IProductDetails productDetails,IManufacturer manufacturer)
        {
            _logger = logger;
            _productRepo = productRepo;
            _productCategoryRepo = productCategory;
            _productDetailsRepo = productDetails;
            _manufacturerRepository = manufacturer;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productRepo.GetAllAsync(); 
            var productCategories = await _productCategoryRepo.GetAllAsync();

            ViewBag.Products = products;
            ViewBag.ProductCategory = productCategories;
            ViewBag.Manu = await _manufacturerRepository.GetAllAsync();
            /*            var products = await _productRepo.GetAllAsync();    */
            return View();
        }
        public async Task<IActionResult> AllProduct()
        {
            var products = await _productRepo.GetAllAsync();
            var productCategories = await _productCategoryRepo.GetAllAsync();

            ViewBag.Products = products;
            ViewBag.ProductCategory = productCategories;
            ViewBag.Manu = await _manufacturerRepository.GetAllAsync();
            /*            var products = await _productRepo.GetAllAsync();    */
            return View("Search", products);
        }
        public async Task<IActionResult> ViewProductWithManufacturer(int id)
        {
            var products = await _productRepo.FindByManufacturerAsync(id);
            ViewBag.Categories = await _productCategoryRepo.GetAllAsync();
            ViewBag.Manu = await _manufacturerRepository.GetAllAsync();
            return View("Search", products);
        }

        public async Task<IActionResult> ViewProductWithCategory(int categoryID)
        {
            var products = await _productRepo.FindByCategoryAsync(categoryID);
            ViewBag.Categories = await _productCategoryRepo.GetAllAsync();
            ViewBag.Manu = await _manufacturerRepository.GetAllAsync();
            return View("Search", products);
        }

        public async Task<IActionResult> ViewProduct(int id)
        {
            var products = await _productRepo.FindByManufacturerAsync(id);
            ViewBag.Manu = await _manufacturerRepository.GetAllAsync();
            return View("Search", products);
        }

        public async Task<IActionResult> Search(string product)
        {
            var products = await _productRepo.FindByNameAsync(product);
            ViewBag.Products = await _productRepo.GetAllAsync();
            ViewBag.Categories = await _productCategoryRepo.GetAllAsync();
            ViewBag.Manu = await _manufacturerRepository.GetAllAsync();
            return View("Search", products);
        }

        public async Task<IActionResult> GetCategoriesView()
        {
            var categories = await _productCategoryRepo.GetAllAsync();
            return View(categories);
        }
        public async Task<IActionResult> ProductsDetailView(int id)
        {
            var productsDetail = await _productDetailsRepo.GetProductDetailsByIdAsync(id);
            return View(productsDetail);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
