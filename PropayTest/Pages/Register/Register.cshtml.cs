using Microsoft.AspNetCore.Mvc.RazorPages;
using PropayTest.Pages.Users;
using PropayTest.Services;
using System.Data.SqlClient;

namespace PropayTest.Pages.Register
{
    public class RegisterModel : PageModel
    {
        public User user = new User();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            user.UserName = Request.Form["register-username"];
            user.Email = Request.Form["register-email"];
            user.FullName = Request.Form["register-name"] + " " + Request.Form["register-surname"];
            user.PasswordHash = PasswordHasher.HashPassword(Request.Form["register-newPassword"]);


            if (UserUniquenessChecker.isUniqueUser(user.UserName, user.Email))
            {
                // save user to database
                try
                {
                    String connectionString = "Data Source=LEGION\\SQLEXPRESS;Initial Catalog=PropayTest;Integrated Security=True";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string sql = "INSERT INTO Users " +
                     "(UserName, FullName, Email, PasswordHash, SecurityStamp, EmailConfirmed, CreatedDate) VALUES " +
                     "(@userName, @fullName, @email, @passwordHash, @securityStamp, @emailConfirmed, @createdDate)";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            // Add parameters to the command
                            command.Parameters.AddWithValue("@userName", user.UserName);
                            command.Parameters.AddWithValue("@fullName", user.FullName);
                            command.Parameters.AddWithValue("@email", user.Email);
                            command.Parameters.AddWithValue("@passwordHash", user.PasswordHash);
                            command.Parameters.AddWithValue("@securityStamp", "stamped"); // Or set this value as needed
                            command.Parameters.AddWithValue("@emailConfirmed", 1); // Assuming 1 means true
                            command.Parameters.AddWithValue("@createdDate", DateTime.Now); // Pass DateTime directly

                            // Execute the command
                            command.ExecuteNonQuery();
                        }
                    }

                }

                catch (Exception e)
                {
                    errorMessage = e.Message;
                    return;
                }

                user.UserName = "";
                user.Email = "";
                user.FullName = "";
                user.PasswordHash = "";
                successMessage = "Created user successfully!";

               /* Console*//*.WriteLine("Posted succesfully the new user");*/
            }
            else
            {
                errorMessage = "pick a unique username/email";
            }
        }
    }
}
