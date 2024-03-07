using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Entities;
using Repository.Repository;

public class EyeglassesModel : PageModel
{
    private readonly UnitOfWork _unitOfWork;

    public List<Eyeglass> eyeglasses = new List<Eyeglass>();
    [BindProperty]
    public int pageIndex { get; set; } = 1;
    public int totalPage { get; set; } = 0;
    [BindProperty]
    public int Price { get; set; }
    [BindProperty]
    public string EyeglassesDescription { get; set; }
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
        eyeglasses = _unitOfWork.EyeGlassRepository.Get(includeProperties: "LensType", pageIndex: pageIndex, pageSize: 4).ToList();
        totalPage = (int)Math.Ceiling((double)_unitOfWork.EyeGlassRepository.CountPage() / 4);
    }
    
    public IActionResult OnPostGetList()
    {
        eyeglasses = _unitOfWork.EyeGlassRepository.Get(includeProperties: "LensType", pageIndex: pageIndex, pageSize: 4).ToList();
        totalPage = (int)Math.Ceiling((double)_unitOfWork.EyeGlassRepository.CountPage() / 4);
        return Page();
    }

    public IActionResult OnPostSearch()
    {
        eyeglasses = _unitOfWork.EyeGlassRepository.Get(x =>
        (Price == null || x.Price == Price)
        || (string.IsNullOrEmpty(EyeglassesDescription) || x.EyeglassesDescription.Contains(EyeglassesDescription)),
        includeProperties: "LensType").ToList();
        return Page();
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

    public IActionResult OnPostCreate()
    {
        return Redirect("/CreateEyeGlasses");
    }
    public IActionResult OnPostUpdate()
    {
        HttpContext.Session.SetString("IdToUpdate", IdToUpdate.ToString());
        return Redirect("/UpdateEyeGlasses");
    }


}