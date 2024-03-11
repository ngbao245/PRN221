using BaoNH.ASM2.Repo.Entities;
using BaoNH.ASM2.Repo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BaoNH.ASM2.Web.Pages.Pizza
{
    public class CategoryModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        public List<Category> Categories { get; set; } = new List<Category>();

        [BindProperty]
        public Category NewCategory { get; set; }

        public CategoryModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {
            Categories = _unitOfWork.CategoryRepository.Get().ToList();
        }

        public IActionResult OnPost()
        {
            _unitOfWork.CategoryRepository.Insert(NewCategory);
            _unitOfWork.Completed();
            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int id)
        {
            var categoryToDelete = _unitOfWork.CategoryRepository.GetByID(id);
            if (categoryToDelete != null)
            {
                _unitOfWork.CategoryRepository.Delete(categoryToDelete);
                _unitOfWork.Completed();
            }
            return RedirectToPage();
        }
    }
}
