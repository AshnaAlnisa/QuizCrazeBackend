using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class viewQuizPerformance
    {
        dbServices ds = new dbServices();

        public async Task<responseData> ViewQuizPerformance(requestData rData)
        {
            responseData resData = new responseData();

            try
            {
                // Assuming 'id' corresponds to the email of the user to fetch results for
                

                var query = @"
                    SELECT 
    qc.quiz_card_id,
    qc.title AS quiz_title,
    COUNT(r.quiz_card_id) AS times_played
FROM 
    quiz_card qc
LEFT JOIN 
    result r ON qc.quiz_card_id = r.quiz_card_id
GROUP BY 
    qc.quiz_card_id, qc.title
ORDER BY 
    times_played DESC";

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
                            quiz_id = rowData[0], 
                            quiz_title = rowData[1],
                            times_played  = rowData[2], 
                        
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
