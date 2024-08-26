using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PropayTest.Pages.Users;
using PropayTest.Services;
using System.Data.SqlClient;

namespace PropayTest.Pages
{
    public class IndexModel : PageModel
    {
        public User user = new User();
        public string errorMessage = "";
        public string successMessage = "";


        public void OnGet()
        {

        }

        public void OnPost()
        {

            

        }
    }

}
