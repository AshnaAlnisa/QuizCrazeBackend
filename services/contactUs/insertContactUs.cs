using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class insertContactUs
    {
        dbServices ds = new dbServices();
        public async Task<responseData> InsertContactUs(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
               
                     var sq=@"insert into quizcraze.contact_us(name,email,message) values(@name,@email,@message)";
                     MySqlParameter[] insertParams = new MySqlParameter[]
                    {
                        new MySqlParameter("@name",rData.addInfo["name"]),
                        new MySqlParameter("@email",rData.addInfo["email"]),
                        new MySqlParameter("@message",rData.addInfo["message"]),
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