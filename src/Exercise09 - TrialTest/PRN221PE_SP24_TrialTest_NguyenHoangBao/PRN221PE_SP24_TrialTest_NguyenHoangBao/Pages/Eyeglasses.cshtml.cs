using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Entities;
using Repository.Repository;
using System.Linq.Expressions;

namespace TrialTest2.Pages
{
    public class EyeglassesModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;

        public List<Eyeglass> eyeglasses = new List<Eyeglass>();

        [BindProperty]
        public int PageIndex { get; set; }
        public int TotalPage { get; set; }

        [BindProperty]
        public string Search { get; set; }

        [BindProperty]
        public int IdToDelete { get; set; }

        [BindProperty]
        public int IdToUpdate { get; set; }

        public EyeglassesModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {
            ReloadPage();
        }

        public IActionResult OnPostGetList()
        {
            ReloadPage();
            return Page();
        }

        public IActionResult OnPostSearch()
        {
            if (string.IsNullOrEmpty(Search))
            {
                ReloadPage();
                return Page();
            }
            else
            {
                eyeglasses = _unitOfWork.EyeGlassRepository.Get(x =>
                    x.Price.ToString() == Search ||
                    string.IsNullOrEmpty(x.EyeglassesDescription) || x.EyeglassesDescription.Contains(Search),
                    includeProperties: "LensType").ToList();
                TotalPage = (int)Math.Ceiling((double)_unitOfWork.EyeGlassRepository.CountPage() / 4);
                return Page();
            }
        }

        public void OnPostDelete()
        {
            var eyeGlassToDelete = _unitOfWork.EyeGlassRepository.GetByID(IdToDelete);
            if (eyeGlassToDelete != null)
            {
                _unitOfWork.EyeGlassRepository.Delete(eyeGlassToDelete);
                _unitOfWork.Completed();
            }
            OnGet();
        }
        public IActionResult OnPostUpdate()
        {
            HttpContext.Session.SetString("UpdateId", IdToUpdate.ToString());
            return Redirect("/Update");
        }

        public IActionResult OnPostCreate()
        {
            return Redirect("/Create");
        }

        private void ReloadPage()
        {
            eyeglasses = _unitOfWork.EyeGlassRepository.Get(includeProperties: "LensType", pageIndex: PageIndex, pageSize: 4).ToList();
            TotalPage = (int)Math.Ceiling((double)_unitOfWork.EyeGlassRepository.CountPage() / 4);
        }
    }
}
