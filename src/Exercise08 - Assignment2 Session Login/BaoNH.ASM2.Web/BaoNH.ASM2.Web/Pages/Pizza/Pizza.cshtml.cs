using BaoNH.ASM2.Repo.Entities;
using BaoNH.ASM2.Repo.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;

namespace BaoNH.ASM2.Web.Pages.Pizza
{
    public class PizzaModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;

        public List<Product> Products { get; set; } = new List<Product>();

        [BindProperty]
        public Product NewProduct { get; set; } = new Product(); // Initialize NewProduct here

        public PizzaModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public BaoNH.ASM2.Repo.Entities.Category GetCategoryForProduct(int? categoryId)
        {
            return _unitOfWork.CategoryRepository.GetByID(categoryId);
        }

        public IActionResult OnGet()
        {
            Products = GetAllProducts();
            ViewData["Categories"] = GetAllCategories();
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (AddProduct(NewProduct))
            {
                NewProduct = new Product();
                return RedirectToPage();
            }
            else
            {
                return Page();
            }
        }

        public IActionResult OnPostDelete(int id)
        {
            if (DeleteProduct(id))
            {
                return RedirectToPage();
            }
            else
            {
                return Page();
            }
        }

        private List<Product> GetAllProducts()
        {
            return _unitOfWork.ProductRepository.GetAll().ToList();
        }

        private List<BaoNH.ASM2.Repo.Entities.Category> GetAllCategories()
        {
            return _unitOfWork.CategoryRepository.GetAll().ToList();
        }

        private bool AddProduct(Product model)
        {
            try
            {
                _unitOfWork.ProductRepository.Insert(model);
                _unitOfWork.Completed();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }


        private bool DeleteProduct(int id)
        {
            var productToDelete = Products.FirstOrDefault(p => p.ProductId == id);
            if (productToDelete != null)
            {
                _unitOfWork.ProductRepository.Delete(productToDelete);
                _unitOfWork.Completed();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
