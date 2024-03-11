using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Entities;
using Repository.Repository;

namespace TrialTest2.Pages
{
    public class UpdateModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;

        [BindProperty]
        public Eyeglass Eyeglass { get; set; }
        public SelectList CategoryOptions { get; set; }

        public UpdateModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            OnGet();
        }
        public void OnGet()
        {
            var categories = _unitOfWork.LensTypeRepository.Get().ToList();
            CategoryOptions = new SelectList(categories, "LensTypeId", "LensTypeName");
        }

        public IActionResult OnPostUpdate()
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

            var IdToUpdate = HttpContext.Session.GetString("UpdateId");
            var existed = _unitOfWork.EyeGlassRepository.GetByID(int.Parse(IdToUpdate));
            if (existed != null)
            {
                existed.Price = Eyeglass.Price;
                existed.Quantity = Eyeglass.Quantity;
                existed.FrameColor = Eyeglass.FrameColor;
                existed.EyeglassesName = Eyeglass.EyeglassesName;
                existed.EyeglassesDescription = Eyeglass.EyeglassesDescription;
                existed.LensTypeId = Eyeglass.LensTypeId;
            }
            _unitOfWork.EyeGlassRepository.Update(existed);
            _unitOfWork.Completed();
            return Redirect("/EyeGlasses");
        }
    }
}
