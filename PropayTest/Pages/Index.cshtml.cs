using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PropayTest.Pages.Users;
using PropayTest.Services;
using System.Data.SqlClient;
namespace PropayTest.Pages
{
    public class IndexModel : PageModel
    {

        public string message = "";

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {


            string userNameOrEmail = Request.Form["login-username-email"]!;
            string password = Request.Form["login-password"]!;


            User user = new User();

            user = UserCredentialsValidator.validAndConfirmed(userNameOrEmail);

            if (user.PasswordHash != null)
            {
                // user
                if (user.PasswordHash == PasswordHasher.HashPassword(password))
                {
                    // user, password
                    if (user.EmailConfirmed)
                    {
                        // great!
                        message = "Signed in!";
                        HttpContext.Session.SetInt32("UserId", user.Id);
                        return Redirect("/profile");

                    }
                    else
                    {
                        // user, password, not confirmed
                        message = "Verify your account first.";
                        return Page();


                    }

                }

                // user, wrong password
                else
                {
                    message = "Wrong password.";
                    return Page();
                }
            }
            else
            {
                // no user
                message = "No account found.";
                return Page();
            }


        }
    }

}
