using System;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using BCrypt.Net;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class changePassword
    {
        private readonly dbServices _dbServices = new dbServices();

        public async Task<responseData> ChangePassword(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var email = rData.addInfo["email"].ToString();
                var currentPassword = rData.addInfo["currentPassword"].ToString();
                var newPassword = rData.addInfo["newPassword"].ToString();

                // Fetch the stored hashed password for the user
                var query = @"SELECT password FROM quizcraze.users WHERE email=@email";
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@email", email)
                };
                
                var dbData = _dbServices.executeSQL(query, myParam);

                if (dbData != null && dbData.Count > 0 && dbData[0].Any())
                {
                    var firstRow = dbData[0].First();
                    var storedHashedPassword = Convert.ToString(firstRow[0]); // Extract password as string

                    // Verify the provided current password
                    bool isCurrentPasswordValid = BCrypt.Net.BCrypt.Verify(currentPassword, storedHashedPassword);

                    if (isCurrentPasswordValid)
                    {
                        // Hash the new password
                        string hashedNewPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);

                        // Update the password in the database
                        var updateQuery = @"UPDATE quizcraze.users SET password=@NEW_PASSWORD WHERE email=@email";
                        MySqlParameter[] updateParams = new MySqlParameter[]
                        {
                            new MySqlParameter("@NEW_PASSWORD", hashedNewPassword),
                            new MySqlParameter("@email", email)
                        };
                        var updateResult = _dbServices.ExecuteUpdateSQL(updateQuery, updateParams);

                        if (updateResult > 0)
                        {
                            resData.rData["rMessage"] = "Password Updated Successfully";
                        }
                        else
                        {
                            resData.rData["rMessage"] = "Failed to update password";
                        }
                    }
                    else
                    {
                        resData.rData["rMessage"] = "Invalid current password";
                    }
                }
                else
                {
                    resData.rData["rMessage"] = "User not found";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "An error occurred: " + ex.Message;
            }
            return resData;
        }
    }
}
