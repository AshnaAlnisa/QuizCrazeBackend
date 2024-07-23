using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class insertCardQuiz
    {
        dbServices ds = new dbServices();

        public async Task<responseData> InsertCardQuiz(requestData rData)
{
    responseData resData = new responseData();
    try
    {
        // Step 1: Insert into quiz_card table
        var insertQuizCardSql = @"INSERT INTO quizcraze.quiz_card (title, no_of_questions) VALUES (@title, @no_of_questions)";
        MySqlParameter[] quizCardParams = new MySqlParameter[]
        {
            new MySqlParameter("@title", rData.addInfo["title"]),
            new MySqlParameter("@no_of_questions", rData.addInfo["no_of_questions"])
        };

        // Execute the SQL and get the result
        var quizCardInsertResult = ds.executeSQL(insertQuizCardSql, quizCardParams);

        // Check if the insertion was successful
        if (quizCardInsertResult != null)
        {
            // Get the last inserted ID
            var quizCardId = GetLastInsertedId();

            if (quizCardId > 0)
            {
                // Step 3: Insert into quizzes table for each question
                foreach (var question in rData.questions)
                {
                    var insertQuizSql = @"INSERT INTO quizcraze.quizzes (quiz_card_id, question, option1, option2, option3, option4, correct_answer)
                                          VALUES (@quiz_card_id, @question, @option1, @option2, @option3, @option4, @correct_answer)";

                    MySqlParameter[] quizParams = new MySqlParameter[]
                    {
                        new MySqlParameter("@quiz_card_id", quizCardId),
                        new MySqlParameter("@question",  rData.addInfo['question']),
                        new MySqlParameter("@option1",  rData.addInfo['option1']),
                        new MySqlParameter("@option2",  rData.addInfo['option2']),
                        new MySqlParameter("@option3",  rData.addInfo['option3']),
                        new MySqlParameter("@option4",  rData.addInfo['option4']),
                        new MySqlParameter("@correct_answer",  rData.addInfo['correct_answer'])
                    };

                    // Execute the SQL for inserting quizzes
                    var quizInsertResult = ds.executeSQL(insertQuizSql, quizParams);
                }

                // Step 4: Update response data
                resData.rData["rMessage"] = "Successful";
            }
            else
            {
                resData.rData["rMessage"] = "Failed to insert quiz_card";
            }
        }
        else
        {
            resData.rData["rMessage"] = "Failed to insert quiz_card";
        }
    }
    catch (Exception ex)
    {
        // Handle exceptions appropriately
        Console.WriteLine($"Exception: {ex.Message}");
        resData.rData["rMessage"] = "Failed: " + ex.Message; // Optionally provide an error message
        throw; // Re-throw the exception to be handled by the caller
    }

    return resData;
}

    private int GetLastInsertedId()
{
    try
    {
        string sql = "SELECT LAST_INSERT_ID() AS last_id";
        var result = ds.executeSQL(sql, null); // Assuming executeSQL returns List<object[]>

        if (result != null && result.Count > 0)
        {
            // Assuming LAST_INSERT_ID() returns a single value, directly access it
            object[] row = result[result.Count - 1]; // Access the last row

            if (row != null && row.Length > 0)
            {
                // Assuming LAST_INSERT_ID() returns a single value in the first column of the last row
                object lastIdValue = row[0];

                // Convert the value to int
                if (lastIdValue != null && lastIdValue != DBNull.Value)
                {
                    return Convert.ToInt32(lastIdValue);
                }
                else
                {
                    throw new Exception("Failed to retrieve last inserted ID");
                }
            }
            else
            {
                throw new Exception("Failed to retrieve last inserted ID");
            }
        }
        else
        {
            throw new Exception("Failed to retrieve last inserted ID");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Exception in GetLastInsertedId: {ex.Message}");
        throw; // Re-throw the exception to be handled by the caller
    }
}




    }
}