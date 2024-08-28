using PropayTest.Pages.Users;
using System.Data.SqlClient;

namespace PropayTest.Services
{
    public class UserService
    {
        public static User GetUserDetails(int Id)
        {
            User user = new User();
            try
            {
                String connectionString = "Data Source=LEGION\\SQLEXPRESS;Initial Catalog=PropayTest;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Users where Id=@Id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // Add parameters to the command
                        command.Parameters.AddWithValue("@Id", Id);

                        // Execute the command
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                
                                user.Id = reader.GetInt32(0);
                                user.UserName = reader.GetString(1);
                                user.FullName = reader.GetString(2);
                                user.Email = reader.GetString(3);
                                user.PasswordHash = reader.GetString(4);
                                user.SecurityStamp = reader.GetString(5);
                                user.EmailConfirmed = reader.GetBoolean(6);
                                user.CreatedDate = reader.GetDateTime(7);
                            }
                        }
                    }
                    
                }

                return user;
            }

            catch (Exception e)
            {
                return user;
            }
        }
    }
}