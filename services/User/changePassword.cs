using System;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class changePassword
    {
        private readonly dbServices _dbServices = new dbServices();

        public async Task<responseData> ChangePassword(requestData rData)
        {
            responseData resData = new responseData();
             try
            {
                var query = @"SELECT * FROM quizcraze.users WHERE email=@email AND password=@CURRENT_PASSWORD";
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@email", rData.addInfo["email"]),
                    new MySqlParameter("@CURRENT_PASSWORD", rData.addInfo["currentPassword"])
                };
                var dbData = _dbServices.executeSQL(query, myParam);
                
                if (dbData[0].Count() > 0)
                {
                    var updateQuery = @"UPDATE quizcraze.users SET password=@NEW_PASSWORD WHERE email=@email";
                    MySqlParameter[] updateParams = new MySqlParameter[]
                    {
                        new MySqlParameter("@NEW_PASSWORD", rData.addInfo["newPassword"]),
                        new MySqlParameter("@email", rData.addInfo["email"])
                    };
                    var updateResult = _dbServices.executeSQL(updateQuery, updateParams);
                    
                    resData.rData["rMessage"] = "Password Updated Successfully";
                }
                else
                {
                    resData.rData["rMessage"] = "Invalid Email id And Current Password";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "An error occurred: " + ex.Message;
            }
            return resData;
        
        }
    }
}
