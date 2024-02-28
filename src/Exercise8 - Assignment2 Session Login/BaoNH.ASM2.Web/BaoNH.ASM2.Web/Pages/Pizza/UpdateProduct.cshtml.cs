using BaoNH.ASM2.Repo.Entities;
using BaoNH.ASM2.Repo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment2.Pages.Pizza
{
    public class UpdateProductModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;

        [BindProperty]
        public Product Product { get; set; }

        public UpdateProductModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult OnGet(int id)
        {
            Product = _unitOfWork.ProductRepository.GetByID(id);
            if (Product == null)
            {
                TempData["ErrorMessage"] = "Product not found.";
                return RedirectToPage("/Pizza/Pizza");
            }

            ViewData["Categories"] = _unitOfWork.CategoryRepository.GetAll();
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                ViewData["Categories"] = _unitOfWork.CategoryRepository.GetAll();
                return Page();
            }

            _unitOfWork.ProductRepository.Update(Product);
            _unitOfWork.Completed();

            TempData["SuccessMessage"] = "Product updated successfully.";
            return RedirectToPage("/Pizza/Pizza");
        }
    }
}
