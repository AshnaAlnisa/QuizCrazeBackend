using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class updateQuiz
    {
        dbServices ds = new dbServices();

        public async Task<responseData> UpdateQuiz(requestData rData)
        {
            responseData resData = new responseData();

            try
            {
                // Your update query
                var query = @"UPDATE quizcraze.quizzes 
                           SET title = @title, total_questions = @total_questions, question = @question, option1 = @option1,  option2 = @option2,  option3 = @option3,  option4 = @option4,  correct_answer = @correct_answer
                           WHERE id = @id;";

                // Your parameters
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                        new MySqlParameter("@id",rData.addInfo["id"]),
                        new MySqlParameter("@title",rData.addInfo["title"]),
                        new MySqlParameter("@total_questions",rData.addInfo["total_questions"]),
                        new MySqlParameter("@question",rData.addInfo["question"]),
                        new MySqlParameter("@option1",rData.addInfo["option1"]),
                        new MySqlParameter("@option2",rData.addInfo["option2"]),
                        new MySqlParameter("@option3",rData.addInfo["option3"]),
                        new MySqlParameter("@option4",rData.addInfo["option4"]),
                        new MySqlParameter("@correct_answer",rData.addInfo["correct_answer"])
                    
                    
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