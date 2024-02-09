using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Models;
using Service.Interfaces;

namespace Assignment2.Pages.Pizza
{
    public class UpdateProductModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        [BindProperty]
        public ProductModel Product { get; set; }

        public UpdateProductModel(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult OnGet(int id)
        {
            Product = _productService.GetAll().FirstOrDefault(_ => _.ProductId.Equals(id));
            if (Product == null)
            {
                TempData["ErrorMessage"] = "Product not found.";
                return RedirectToPage("/Pizza/Pizza");
            }

            ViewData["Categories"] = _categoryService.GetAll();
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                ViewData["Categories"] = _categoryService.GetAll();
                return Page();
            }

            var success = _productService.UpdateProduct(Product);
            if (success)
            {
                TempData["SuccessMessage"] = "Product updated successfully.";
                return RedirectToPage("/Pizza/Pizza");
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update the product.";
                ViewData["Categories"] = _categoryService.GetAll();
                return Page();
            }
        }
    }
}
