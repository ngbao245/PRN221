using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Interfaces;

namespace Assignment2.Pages.Pizza
{
    [Authorize]
    public class CategoryModel : PageModel
    {
        private readonly ICategoryService _categoryService;
        public List<Repository.Models.CategoryModel> Categories { get; set; } = new List<Repository.Models.CategoryModel>();

        [BindProperty]
        public Repository.Models.CategoryModel NewCategory { get; set; }

        public CategoryModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public void OnGet()
        {
            Categories = _categoryService.GetAll().ToList();
        }

        public IActionResult OnPost()
        {
            _categoryService.AddCategory(NewCategory);
            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int id)
        {
            var categoryToDelete = _categoryService.GetAll().FirstOrDefault(p => p.CategoryId.Equals(id));
            if (categoryToDelete != null)
            {
                _categoryService.DeleteCategory(categoryToDelete.CategoryId);
            }
            return RedirectToPage();
        }
    }
}
