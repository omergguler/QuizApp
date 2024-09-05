using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PropayTest.Pages.Users;
using PropayTest.Services;
using PropayTest.Models.Question;
using PropayTest.Models.Quiz;
using System.Data.SqlClient;


namespace PropayTest.Pages.Profile
{
    public class ProfileModel : PageModel
    {
        public User? User { get; set; }
        public List<Question> Questions { get; set; }

        public string errorMessage = "";
        public string successMessage = "";

        public List<int> SelectedQuestionIds { get; set; } = new List<int>();


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
            TempData["SuccessMessage"] = null;
            return RedirectToPage("/Index");
        }

        public IActionResult OnPostPickQuestions(List<int> SelectedQuestionIds)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId != null)
            {
                try
                {
                    String connectionString = "Data Source=LEGION\\SQLEXPRESS;Initial Catalog=PropayTest;Integrated Security=True";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string sql = "INSERT INTO Quizzes " +
                     "(Title, Description, CreatedDate, IsActive, CreatorId) VALUES " +
                     "(@title, @description, @createdDate, @isActive, @creatorId)";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            // Add parameters to the command
                            command.Parameters.AddWithValue("@title", "title");
                            command.Parameters.AddWithValue("@description", "description");
                            command.Parameters.AddWithValue("@createdDate", DateTime.Now);
                            command.Parameters.AddWithValue("@isActive", 1);
                            command.Parameters.AddWithValue("@creatorId", userId); // Or set this value as needed

                            // Execute the command
                            command.ExecuteNonQuery();
                        }
                    }

                    

                }

                catch (Exception e)
                {
                    errorMessage = e.Message;
                }

                /* public List<Question> PickedQuestions = new List<Question>();*/

                HttpContext.Session.SetInt32("UserId", (int)userId);
                TempData["SuccessMessage"] = "Created quiz!";
                return Redirect("/profile");

            }

            else
            {
                return RedirectToPage("/Index");
            }


        }

    }
}
