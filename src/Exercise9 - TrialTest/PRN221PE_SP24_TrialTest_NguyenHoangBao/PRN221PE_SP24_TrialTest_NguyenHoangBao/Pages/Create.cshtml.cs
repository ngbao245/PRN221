using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Entities;
using Repository.Repository;

namespace TrialTest2.Pages
{
    public class CreateModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;

        [BindProperty]
        public Eyeglass Eyeglass { get; set; }
        public SelectList CategoryOptions { get; set; }

        public CreateModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            OnGet();
        }

        public void OnGet()
        {
            var categories = _unitOfWork.LensTypeRepository.Get().ToList();
            CategoryOptions = new SelectList(categories, "LensTypeId", "LensTypeName");
        }

        public IActionResult OnPostCreate()
        {
            if (string.IsNullOrWhiteSpace(Eyeglass.EyeglassesName) ||
                string.IsNullOrWhiteSpace(Eyeglass.EyeglassesDescription) ||
                string.IsNullOrWhiteSpace(Eyeglass.FrameColor) ||
                CategoryOptions == null)
            {
                ModelState.AddModelError(string.Empty, "All fields are required");
                return Page();
            }
            if (Eyeglass.Quantity > 999 || Eyeglass.Quantity < 0)
            {
                ModelState.AddModelError(string.Empty, "Value for Quantity <= 999 and >= 0");
                return Page();
            }
            if (Eyeglass.EyeglassesName.Length <= 10)
            {
                ModelState.AddModelError(string.Empty, "Value for eyeglasses’ name is greater than 10 characters");
                return Page();
            }
            if (Eyeglass.EyeglassesName.Split(' ').Any(word => char.IsLower(word[0])))
            {
                ModelState.AddModelError(string.Empty, "Each word of the EyeglassesName must begin with the capital letter");
                return Page();
            }
            Eyeglass.CreatedDate = DateTime.Now;

            _unitOfWork.EyeGlassRepository.Insert(Eyeglass);
            _unitOfWork.Completed();
            return Redirect("/EyeGlasses");
        }
    }
}
