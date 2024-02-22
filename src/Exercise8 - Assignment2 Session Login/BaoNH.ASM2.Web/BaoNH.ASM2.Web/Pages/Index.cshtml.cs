using BaoNH.ASM2.Repo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BaoNH.ASM2.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public IndexModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult OnPost()
        {
            if (IsValidLogin(Username, Password))
            {
                return RedirectToPage("/Admin");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return Page();
            }
        }

        private bool IsValidLogin(string username, string password)
        {
            var user = _unitOfWork.UserRepository.Get(u => u.UserName == username).FirstOrDefault();
            return user != null && user.Password == password;
        }
    }
}
