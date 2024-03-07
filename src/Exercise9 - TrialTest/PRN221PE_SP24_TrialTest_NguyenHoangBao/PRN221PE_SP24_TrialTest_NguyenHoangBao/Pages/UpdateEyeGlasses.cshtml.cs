using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Entities;
using Repository.Repository;

namespace PRN221PE_SP24_TrialTest_NguyenHoangBao.Pages
{
    public class UpdateEyeGlassModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;

        [BindProperty]
        public Eyeglass Eyeglass { get; set; }
        public SelectList CategoryOptions { get; set; }
        public UpdateEyeGlassModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet()
        {
            var categories = _unitOfWork.LensTypeRepository.Get().ToList();
            CategoryOptions = new SelectList(categories, "LensTypeId", "LensTypeName");
        }

        public IActionResult OnPost()
        {
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
            var words = Eyeglass.EyeglassesName.Split(' ');
            foreach (var word in words)
            {
                if (char.IsLower(word[0]))
                {
                    ModelState.AddModelError(string.Empty, "Each word of the EyeglassesName must begin with the capital letter");
                    return Page();// Add only one error for the entire EyeglassesName
                }
            }
            var IdToUpdate = HttpContext.Session.GetString("IdToUpdate");
            var existedEyeGlass = _unitOfWork.EyeGlassRepository.GetByID(int.Parse(IdToUpdate));
            if (existedEyeGlass != null)
            {
                existedEyeGlass.Price = Eyeglass.Price;
                existedEyeGlass.Quantity = Eyeglass.Quantity;
                existedEyeGlass.FrameColor = Eyeglass.FrameColor;
                existedEyeGlass.EyeglassesName = Eyeglass.EyeglassesName;
                existedEyeGlass.EyeglassesDescription = Eyeglass.EyeglassesDescription;
                existedEyeGlass.LensTypeId = Eyeglass.LensTypeId;
            }
            _unitOfWork.EyeGlassRepository.Update(existedEyeGlass);
            _unitOfWork.Completed();
            return Redirect("/EyeGlasses");
        }
    }
}
