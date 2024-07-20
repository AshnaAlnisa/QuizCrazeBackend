using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class insertResult
    {
        dbServices ds = new dbServices();
        public async Task<responseData> InsertResult(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
               
                     var sq=@"insert into quizcraze.result(user_id,quiz_card_id,correct_answer, incorrect_answer, score) values(@user_id,@quiz_card_id,@correct_answer, @incorrect_answer, @score)";
                     MySqlParameter[] insertParams = new MySqlParameter[]
                    {
                        new MySqlParameter("@user_id",rData.addInfo["user_id"]),
                        new MySqlParameter("@quiz_card_id",rData.addInfo["quiz_card_id"]),
                        new MySqlParameter("@correct_answer",rData.addInfo["correct_answer"]),
                        new MySqlParameter("@incorrect_answer",rData.addInfo["incorrect_answer"]),
                        new MySqlParameter("@score",rData.addInfo["score"])
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