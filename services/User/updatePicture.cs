using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class updatePicture
    {
        dbServices ds = new dbServices();


        public async Task<responseData> UpdatePicture(requestData rData)
        {
            responseData resData = new responseData();

             try
        {
            // Your update query for the profile picture
            var query = @"UPDATE quizcraze.users 
                          SET picture = @picture 
                          WHERE id = @Id;";

            // Your parameters
            MySqlParameter[] myParam = new MySqlParameter[]
            {
                new MySqlParameter("@Id", rData.addInfo["id"]),
                new MySqlParameter("@picture", rData.addInfo["picture"])
            };

            // Condition to execute the update query
            bool shouldExecuteUpdate = true;

            if (shouldExecuteUpdate)
            {
                int rowsAffected = ds.ExecuteUpdateSQL(query, myParam);

                if (rowsAffected > 0)
                {
                    resData.rData["rMessage"] = "Profile picture updated successfully.";
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