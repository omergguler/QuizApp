using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PropayTest.Pages.Users;

namespace PropayTest.Pages.Library
{
    public class LibraryModel : PageModel
    {
        public User? User { get; set; }

        public void OnGet()
        {
        }
    }
}
