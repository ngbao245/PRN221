using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Models;
using Service.Interfaces;
using Service.Services;

namespace Assignment2.Pages.Pizza
{
    [Authorize]
    public class PizzaModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public List<ProductModel> Products { get; set; } = new List<ProductModel>();

        [BindProperty]
        public ProductModel NewProduct { get; set; }
        public ProductModel UpdateProduct { get; set; }

        public PizzaModel(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public Repository.Models.CategoryModel GetCategoryForProduct(int? categoryId)
        {
            return _categoryService.GetCategoryById(categoryId);
        }

        public IActionResult OnGet()
        {
            Products = _productService.GetAll().ToList();
            ViewData["Categories"] = _categoryService.GetAll().ToList();
            return Page();
        }

        public IActionResult OnPost()
        {
            _productService.AddProduct(NewProduct);
            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int id)
        {
            var productToDelete = _productService.GetAll().FirstOrDefault(p => p.ProductId.Equals(id));
            if (productToDelete != null)
            {
                _productService.DeleteProduct(productToDelete.ProductId);
            }
            return RedirectToPage();
        }
    }
}
