using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Repository;

namespace TrialTest2.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;

        [BindProperty]
        public LoginModel LoginModel { get; set; }

        public IndexModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult OnPost()
        {
            var existAccount = _unitOfWork.StoreAccountRepository.Get(
                               _ => _.EmailAddress.Equals(LoginModel.EmailAddress) &&
                               _.AccountPassword.Equals(LoginModel.AccountPassword) &&
                               (_.Role == 1 || _.Role == 2)).FirstOrDefault();

            if (existAccount != null)
            {
                HttpContext.Session.SetString("Email", existAccount.EmailAddress);
                HttpContext.Session.SetString("Role", existAccount.Role.ToString());
                return Redirect("/Eyeglasses");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Log in error");
                return Page();
            }
        }
}

public class LoginModel
{
    public string EmailAddress { get; set; }
    public string AccountPassword { get; set; }
}
}
