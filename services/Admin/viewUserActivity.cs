using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class viewUserActivity
    {
        dbServices ds = new dbServices();

        public async Task<responseData> ViewUserActivity(requestData rData)
        {
            responseData resData = new responseData();

            try
            {
                // Assuming 'id' corresponds to the email of the user to fetch results for
                

                var query = @"
                    SELECT 
    u.user_id,
    u.username,
    u.name,
    COUNT(DISTINCT r.quiz_card_id) AS total_quizzes_taken
FROM 
    users u
LEFT JOIN 
    result r ON u.user_id = r.user_id
GROUP BY 
    u.user_id, u.username, u.name
ORDER BY 
    total_quizzes_taken DESC";

                // MySqlParameter[] myParams = new MySqlParameter[]
                // {
                //     new MySqlParameter("@email", rData.addInfo["email"])
                // };

                var dbData = ds.executeSQL(query, null);

                List<object> itemsList = new List<object>();

                foreach (var rowSet in dbData)
                {
                    foreach (var row in rowSet)
                    {
                        List<string> rowData = new List<string>();

                        foreach (var column in row)
                        {
                            // Ensure that column is properly cast to string if necessary
                            string columnValue = column.ToString(); // Explicitly convert to string if column is not already a string
                            rowData.Add(columnValue);
                        }

                        // Construct item object
                        var item = new
                        {
                            user_id = rowData[0], 
                            username = rowData[1],
                            name  = rowData[2], 
                            total_quizzes_taken  = Convert.ToInt32(rowData[3]), 
                        
                        };

                        itemsList.Add(item);
                    }
                }

                resData.rData["items"] = itemsList;
                resData.rData["rMessage"] = "Successful";
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "Exception occurred: " + ex.Message;
            }

            return resData;
        }
    }
}
