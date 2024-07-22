using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class viewQuizByQuizCardId
    {
        dbServices ds = new dbServices();
        public async Task<responseData>ViewQuizByQuizCardId(requestData rData)

 {
            responseData resData = new responseData();

             try
            {
                // var query = @"SELECT * FROM detailsdb.destination_card WHERE id=@id";
                var query = @"SELECT * FROM quizcraze.quizzes WHERE quiz_card_id = @quiz_card_id;";
                // Add WHERE clause if filtering by email
                // query += " WHERE email = @Email";

                // MySqlParameter[] myParam = new MySqlParameter[] {
                //     new MySqlParameter("@id",req.addInfo["id"]),
                // };
                 MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@quiz_card_id", rData.addInfo["quiz_card_id"])
                };

                var dbData = ds.executeSQL(query, myParam); // pass myParam if filtering by email

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
                            quiz_id = rowData[0],
                            quiz_card_id = rowData[1],
                            question = rowData[2],
                            option1 = rowData[3],
                            option2 = rowData[4],
                            option3 = rowData[5],
                            option4 = rowData[6],
                            correct_answer = rowData[7],
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









