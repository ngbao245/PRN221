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

        //public IActionResult OnPostDelete(int id)
        //{
        //    var personToDelete = _dbContext.People.FirstOrDefault(p => p.Id == id);
        //    if (personToDelete != null)
        //    {
        //        _dbContext.People.Remove(personToDelete);
        //        _dbContext.SaveChanges();
        //    }
        //    return RedirectToPage();
        //}
    }
}
