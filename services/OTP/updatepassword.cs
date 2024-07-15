using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Text.Json;
using System.Net;
using System.Net.Mail;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
   public class updatepassword
{
    public  dbServices ds = new dbServices();

   public async Task<responseData> Updatepassword(requestData rData)
        {
            responseData resData = new responseData();

            string connectionString = "server=127.0.0.1;user=root;password=root;port=3306;database=detailsdb; Max Pool Size=200;";
            try
            {
                string email = rData.addInfo["EMAILID"].ToString();
                string newPassword = rData.addInfo["PASSWORD"].ToString();

                // Update password in the database
                var updatePasswordQuery = @"UPDATE detailsdb.signuplogin SET PASSWORD = @PASSWORD WHERE EMAILID = @EMAILID";

                using (var connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (var cmd = new MySqlCommand(updatePasswordQuery, connection))
                    {
                        // Implement your password hashing logic here if necessary
                        cmd.Parameters.AddWithValue("@PASSWORD", HashPassword(newPassword));
                        cmd.Parameters.AddWithValue("@EMAILID", email);

                        int rowsAffected = await cmd.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            resData.rData["success"] = true;
                            resData.rData["message"] = "Password updated successfully.";
                        }
                        else
                        {
                            resData.rData["message"] = "Failed to update password.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resData.rData["message"] = "Exception occurred: " + ex.Message;
                Console.WriteLine("Exception in UpdatePassword: " + ex.Message);
            }

            return resData;
        }

    
        private string HashPassword(string password)
        {
            
            return password; 
        }
    }
}