using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PropayTest.Pages.Users;
using PropayTest.Services;
using PropayTest.Models.Question;


namespace PropayTest.Pages.Profile
{
    public class ProfileModel : PageModel
    {
        public User? User { get; set; }
        public List<Question> Questions { get; set; }

        public string errorMessage = "";

        public void OnGet()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId != null)
            {
                // Fetch user details from the database using userId
                User = UserService.GetUserDetails((int)userId);
                Questions = QuestionService.GetAllQuestions((int)userId);
                if (Questions.Count < 5)
                {
                    errorMessage = "You need to have created at least 5 questions. You currently have " + Questions.Count;
                }
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
