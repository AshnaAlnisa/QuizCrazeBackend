using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class viewLeaderboard
    {
        dbServices ds = new dbServices();

        public async Task<responseData> ViewLeaderboard(requestData req)
        {
            responseData resData = new responseData();

            try
            {
                // SQL query to fetch top 5 users based on quiz scores
                var query = @"
                    SELECT 
                    u.username,
                    u.name,
                    qc.title AS quiz_title,
                    r.score
                    FROM result r
                    JOIN users u ON r.user_id = u.user_id
                    JOIN quiz_card qc ON r.quiz_card_id = qc.quiz_card_id
                    ORDER BY r.score DESC
                    LIMIT 10";

                var dbData = ds.executeSQL(query, null); // No parameters needed for this query

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
                            username = rowData[0], // Assuming rowData[0] corresponds to the username column
                            name = rowData[1], // Assuming rowData[1] corresponds to the name column
                            quiz_title = rowData[2], // Assuming rowData[2] corresponds to the quiz_title column
                            score = Convert.ToInt32(rowData[3]) // Assuming rowData[3] corresponds to the score column
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
