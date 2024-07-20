using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class viewCardQuiz
    {
        dbServices ds = new dbServices();
        public async Task<responseData>ViewCardQuiz(requestData req)

 {
            responseData resData = new responseData();

             try
            {
                // var query = @"SELECT * FROM detailsdb.destination_card WHERE id=@id";
                var query = @"SELECT * FROM quizcraze.quiz_card";
                // Add WHERE clause if filtering by email
                // query += " WHERE email = @Email";

                // MySqlParameter[] myParam = new MySqlParameter[] {
                //     new MySqlParameter("@id",req.addInfo["id"]),
                // };

                var dbData = ds.executeSQL(query, null); // pass myParam if filtering by email

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
                            quiz_card_id = rowData[0],
                            title = rowData[1],
                            no_of_questions = rowData[2],
                           
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









