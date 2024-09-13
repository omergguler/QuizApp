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

        public IActionResult OnPostCreateQuestion(string category, string questionText, string choiceOne, string choiceTwo, string choiceThree, string choiceFour, string answer)
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
                        string sql = "INSERT INTO Questions " +
                     "(QuestionText, Choice1, Choice2, Choice3, Choice4, CorrectChoice, CreatorId, CreatedDate, HowManyAnsweredWrong, HowManyAnsweredCorrect, Category) VALUES " +
                     "(@questionText, @choice1, @choice2, @choice3, @choice4, @correctChoice, @creatorId, @createdDate, @howManyWrong, @howManyCorrect, @category)";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            // Add parameters to the command
                            command.Parameters.AddWithValue("@questionText", questionText);
                            command.Parameters.AddWithValue("@choice1", choiceOne);
                            command.Parameters.AddWithValue("@choice2", choiceTwo);
                            command.Parameters.AddWithValue("@choice3", choiceThree);
                            command.Parameters.AddWithValue("@choice4", choiceFour);
                            command.Parameters.AddWithValue("correctChoice", answer);
                            command.Parameters.AddWithValue("@creatorId", userId);
                            command.Parameters.AddWithValue("@createdDate", DateTime.Now);
                            command.Parameters.AddWithValue("@howManyWrong", 0);
                            command.Parameters.AddWithValue("@howManyCorrect", 0);
                            command.Parameters.AddWithValue("@category", category);

                            // Execute the command
                            command.ExecuteNonQuery();
                        }
                    }
                    TempData["SuccessMessage"] = "Created question!";
                    TempData["OpenSecondModal"] = "open";
                    return Redirect("/profile");

                }

                catch (Exception e)
                {
                    errorMessage = e.Message;
                    return Page();
                }

                /* public List<Question> PickedQuestions = new List<Question>();*/

                

            }

            else
            {
                //return RedirectToPage("/Index");
                return Page();
            }
        }


        public IActionResult OnPostSignOut()
        {
            HttpContext.Session.Remove("UserId");
            User = null;
            TempData["SuccessMessage"] = null;
            return RedirectToPage("/Index");
        }


        public IActionResult OnPostDeleteQuestion(int SelectedQuestionId)
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
                        string sql = "Delete from Questions where QuestionId = @QuestionId";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            // Add parameters to the command
                            command.Parameters.AddWithValue("@QuestionId", SelectedQuestionId);

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
                TempData["SuccessMessage"] = "Deleted question";
                TempData["OpenSecondModal"] = "open";
                return Redirect("/profile");

            }

            else
            {
                return RedirectToPage("/Index");
            }


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

                    HttpContext.Session.SetInt32("UserId", (int)userId);
                    TempData["SuccessMessage"] = "Created quiz!";
                    TempData["OpenSecondModal"] = "open";
                    return Redirect("/profile");

                }

                catch (Exception e)
                {
                    errorMessage = e.Message;
                }

                /* public List<Question> PickedQuestions = new List<Question>();*/
                return Page();
                

            }

            else
            {
                return RedirectToPage("/Index");
            }


        }

    }
}
