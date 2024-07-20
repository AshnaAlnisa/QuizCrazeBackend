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
               
                     var sq=@"insert into quizcraze.quiz_card(title,no_of_questions) values(@title,@no_of_questions)";
                     MySqlParameter[] insertParams = new MySqlParameter[]
                    {
                        new MySqlParameter("@title",rData.addInfo["title"]),
                        new MySqlParameter("@no_of_questions",rData.addInfo["no_of_questions"])
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