using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class insertQuestion
    {
        dbServices ds = new dbServices();
        public async Task<responseData> InsertQuestion(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
               
                     var sq=@"insert into quizcraze.quizzes(title,question, option1, option2, option3, option4, correct_answer) values(@title,@question, @option1, @option2, @option3, @option4, @correct_answer)";
                     MySqlParameter[] insertParams = new MySqlParameter[]
                    {
                        new MySqlParameter("@title",rData.addInfo["title"]),
                        new MySqlParameter("@question",rData.addInfo["question"]),
                        new MySqlParameter("@option1",rData.addInfo["option1"]),
                        new MySqlParameter("@option2",rData.addInfo["option2"]),
                        new MySqlParameter("@option3",rData.addInfo["option3"]),
                        new MySqlParameter("@option4",rData.addInfo["option4"]),
                        new MySqlParameter("@correct_answer",rData.addInfo["correct_answer"])
                    };
                    var insertResult = ds.executeSQL(sq, insertParams);

                    resData.rData["rMessage"] = "Successful";
                    
            }
            catch (Exception ex)
            {

                throw;
            }
            return resData;
        }

    }
}