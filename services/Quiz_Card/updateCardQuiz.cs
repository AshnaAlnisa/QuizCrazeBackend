using System;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class updateCardQuiz
    {
        dbServices ds = new dbServices(); // Assuming dbServices is a class handling database operations

        public async Task<responseData> UpdateCardQuiz(requestData rData)
        {
            responseData resData = new responseData();

            try
            {
                // Your update query
                var query = @"UPDATE quiz_card 
                              SET title = @title, no_of_questions = @no_of_questions
                              WHERE quiz_card_id = @quiz_card_id;";

                // Your parameters
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@quiz_card_id", rData.addInfo["quiz_card_id"]),
                    new MySqlParameter("@title", rData.addInfo["title"]),
                    new MySqlParameter("@no_of_questions", rData.addInfo["no_of_questions"])
                };

                // Execute the update query
                int rowsAffected = ds.ExecuteUpdateSQL(query, myParam);

                if (rowsAffected > 0)
                {
                    resData.rData["rMessage"] = "Update successful.";
                }
                else
                {
                    resData.rData["rMessage"] = "No rows affected. Update failed.";
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
