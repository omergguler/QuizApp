using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PropayTest.Models.Question;
using PropayTest.Models.Quiz;
using PropayTest.Pages.Users;
using PropayTest.Services;

namespace PropayTest.Pages.Library
{
    public class LibraryModel : PageModel
    {
        public User? User { get; set; }
        public List<Quiz> Quizzes { get; set; }

        public List<Question> Questions { get; set; }

        public void OnGet()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId != null)
            {
                // Fetch user details from the database using userId
                User = UserService.GetUserDetails((int)userId);
                Quizzes = QuizService.GetAllQuizzes((int)userId);
                Questions = QuestionService.GetAllQuestions((int)userId);
                //Questions = QuestionService.GetAllQuestions((int)userId);
                //if (Questions.Count < 5)
                //{
                //    errorMessage = "You need to have created at least 5 questions. You currently have " + Questions.Count;
                //}
            }
        }
    }
}
