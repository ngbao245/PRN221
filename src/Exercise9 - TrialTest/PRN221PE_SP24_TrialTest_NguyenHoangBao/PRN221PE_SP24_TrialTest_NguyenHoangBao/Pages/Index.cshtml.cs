using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Repository.Repository;
using PRN221PE_SP24_TrialTest_NguyenHoangBao;

public class IndexModel : PageModel
{
    private readonly UnitOfWork _unitOfWork;

    [BindProperty]
    public RequestLoginModel RequestLoginModel { get; set; }
    public IndexModel(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult OnPost()
    {
        var existLoginAccount = _unitOfWork.StoreAccountRepository.Get
            (x => x.EmailAddress.Equals(RequestLoginModel.EmailAddress)
            && x.AccountPassword.Equals(RequestLoginModel.Password)
            && (x.Role == 1 || x.Role == 2)).FirstOrDefault();
        if (existLoginAccount != null)
        {
            HttpContext.Session.SetString("Email", existLoginAccount.EmailAddress);
            HttpContext.Session.SetString("Role", existLoginAccount.Role.ToString());
            return RedirectToPage("/Eyeglasses");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Log in error");
            return Page();
        }
    }
}
