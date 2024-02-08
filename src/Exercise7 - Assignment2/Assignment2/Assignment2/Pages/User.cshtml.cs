using CodeInBlue.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeInBlue.Pages
{
    [Authorize]
    public class UserModel : PageModel
    {
        public UserManager<ApplicationUser> _userManager;

        public ApplicationUser? appUser { get; set; }
        public UserModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public void OnGet()
        {
            var task = _userManager.GetUserAsync(User);
            task.Wait();
            appUser = task.Result;
        }
    }
}
