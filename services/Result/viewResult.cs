using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class viewResult
    {
        dbServices ds = new dbServices();

        public async Task<responseData> ViewResult(requestData rData)
        {
            responseData resData = new responseData();

            try
            {
                // string desiredUsername = req.addInfo["username"]; // Assuming 'username' is passed in the requestData

                // SQL query to fetch quiz names and scores for a specific user
                var query =  @"
                    SELECT qc.title AS quiz_title,
                    r.correct_answer,
                    r.incorrect_answer,
                    r.score,
                    r.result_id,
                    r.quiz_date
                    FROM result r
                    JOIN users u ON r.user_id = u.user_id
                    JOIN quiz_card qc ON r.quiz_card_id = qc.quiz_card_id
                    WHERE u.email = @email";

                MySqlParameter[] myParam = new MySqlParameter[] {
                    new MySqlParameter("@email", rData.addInfo["email"])
                };

                var dbData = ds.executeSQL(query, myParam);

                List<object> itemsList = new List<object>();

                foreach (var rowSet in dbData)
                {
                    foreach (var row in rowSet)
                    {
                        List<string> rowData = new List<string>();

                        foreach (var column in row)
                        {
                            rowData.Add(column.ToString());
                        }

                        // Construct item object
                        var item = new
                        {
                            quiz_name = rowData[0],
                            correct_answer = Convert.ToInt32(rowData[1]),
                            incorrect_answer = Convert.ToInt32(rowData[2]),
                            score = Convert.ToInt32(rowData[3]),
                            result_id = Convert.ToInt32(rowData[4]),
                            quiz_date = rowData[5]
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
