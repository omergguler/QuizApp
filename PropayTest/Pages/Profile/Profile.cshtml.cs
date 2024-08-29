using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PropayTest.Pages.Users;
using PropayTest.Services;


namespace PropayTest.Pages.Profile
{
    public class ProfileModel : PageModel
    {
        public User? User { get; set; }

        public void OnGet()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId != null)
            {
                // Fetch user details from the database using userId
                User = UserService.GetUserDetails((int)userId);
            }
        }

        public IActionResult OnPostSignOut()
        {
            HttpContext.Session.Remove("UserId");
            User = null;
            return RedirectToPage("/Index");
        }
    }
}
