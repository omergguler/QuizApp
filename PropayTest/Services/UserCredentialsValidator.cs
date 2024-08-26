using PropayTest.Pages.Users;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace PropayTest.Services
{
    public class UserCredentialsValidator
    {
        public static User validAndConfirmed(string userNameOrEmail)
        {
            User user = new User();
            
            try
            {
                String connectionString = "Data Source=LEGION\\SQLEXPRESS;Initial Catalog=PropayTest;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Users where UserName = @userNameOrEmail or Email = @userNameOrEmail";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // Add parameters to the command
                        command.Parameters.AddWithValue("@userNameOrEmail", userNameOrEmail);
                        //command.Parameters.AddWithValue("@passwordHash", PasswordHasher.HashPassword(password));

                        
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

            }

            catch (Exception e)
            {
                return user;
            }

            return user;
        }
    }
}
