using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class signup
    {
        dbServices ds = new dbServices();
        public async Task<responseData> Signup(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"SELECT * FROM detailsdb.signuplogin WHERE EMAILID = @EMAILID";

                MySqlParameter[] myParam = new MySqlParameter[]
                {
                new MySqlParameter("@NAME",rData.addInfo["NAME"]),
                new MySqlParameter("@EMAILID", rData.addInfo["EMAILID"]),
                new MySqlParameter("@PASSWORD",rData.addInfo["PASSWORD"]),
            
                };
                var dbData = ds.executeSQL(query, myParam);
                if (dbData[0].Count() > 0)
                {
                    resData.rData["rMessage"] = "User Already Registered";
                }
                else
                {
                    var sq=@"insert into detailsdb.signuplogin(NAME, EMAILID, PASSWORD) values(@NAME,@EMAILID,@PASSWORD)";
                    
                    var insertResult = ds.executeSQL(sq, myParam);

                    resData.rData["rMessage"] = "Sign Up Successfull";
                    
                }

            }
            catch (Exception ex)
            {

                throw;
            }
            return resData;
        }

        private string GetStringFromAddInfo(requestData rData, string key)
        {
            if (rData.addInfo.ContainsKey(key))
            {
                return rData.addInfo[key].ToString();
            }
            else
            {
                return "N/A";
            }
        }
    }
}