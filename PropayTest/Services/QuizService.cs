using PropayTest.Models.Question;
using PropayTest.Models.Quiz;
using System.Data.SqlClient;

namespace PropayTest.Services
{
    public class QuizService
    {

        public static List<Quiz> GetAllQuizzes(int UserId)
        {
            List<Quiz> quizzes = new List<Quiz>();
            try
            {
                String connectionString = "Data Source=LEGION\\SQLEXPRESS;Initial Catalog=PropayTest;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Quizzes where CreatorId=@creatorId";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // Add parameters to the command
                        command.Parameters.AddWithValue("@creatorId", UserId);

                        // Execute the command
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                Quiz quiz = new Quiz();
                                quiz.Id = reader.GetInt32(0);
                                quiz.Title = reader.GetString(1);
                                quiz.Description = reader.GetString(2);
                                quiz.CreatedDate = reader.GetDateTime(3);
                                quiz.IsActive = reader.GetBoolean(4);
                                quiz.CreatorId = reader.GetInt32(5);
                                
                                quizzes.Add(quiz);
                            }
                        }
                    }
                }

            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return quizzes;
        }

    }
}
