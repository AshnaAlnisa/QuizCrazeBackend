using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class updateUser
    {
        dbServices ds = new dbServices();

        public async Task<responseData> UpdateUser(requestData rData)
        {
            responseData resData = new responseData();

            try
            {
                // Your update query
                var query = @"UPDATE quizcraze.users 
                           SET username = @username, name = @name, email = @email, address = @address, picture = @picture
                           WHERE user_id = @user_id;";

                // Your parameters
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@user_id", rData.addInfo["user_id"]),
                    new MySqlParameter("@username", rData.addInfo["username"]),
                     new MySqlParameter("@name", rData.addInfo["name"]),
                     new MySqlParameter("@email", rData.addInfo["email"]),
                    new MySqlParameter("@address", rData.addInfo["address"]),
                    new MySqlParameter("@picture", rData.addInfo["picture"]),
                    
                    
                };

                // Condition to execute the update query
                bool shouldExecuteUpdate = true;

                if (shouldExecuteUpdate)
                {
                    int rowsAffected = ds.ExecuteUpdateSQL(query, myParam);

                    if (rowsAffected > 0)
                    {
                        resData.rData["rMessage"] = "UPDATE SUCCESSFULLY.";
                    }
                    else
                    {
                        resData.rData["rMessage"] = "No rows affected. Update failed.";
                    }
                }
                else
                {
                    resData.rData["rMessage"] = "Condition not met. Update query not executed.";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "Exception occurred: " + ex.Message;
            }
            return resData;
        }

       
    }
}