using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class fetchRegister
    {
        dbServices ds = new dbServices();
        public async Task<responseData> FetchRegister(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"SELECT * FROM detailsdb.details where EMAILID=@EMAILID";
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                new MySqlParameter("@NAME",rData.addInfo["NAME"]),
                new MySqlParameter("@COUNTRY",rData.addInfo["COUNTRY"]),
                new MySqlParameter("@EMAILID",rData.addInfo["EMAILID"]),
                new MySqlParameter("@TOURDESCRIPTIONS",rData.addInfo["TOURDESCRIPTIONS"]),
                new MySqlParameter("@TRAVELDATES",rData.addInfo["TRAVELDATES"]),
                new MySqlParameter("@DURATIONOFTHESTAY",rData.addInfo["DURATIONOFTHESTAY"]),
                new MySqlParameter("@NOOFPERSON",rData.addInfo["NOOFPERSON"]),
                new MySqlParameter("@CONTACTNO",rData.addInfo["CONTACTNO"]),
            
                };
                var dbData = ds.executeSQL(query, myParam);
                if (dbData[0].Count() > 0)
                {
                    resData.rData["rMessage"] = "Duplicate Credentials";
                }
                else
                {
                    var sq=@"insert into detailsdb.details(NAME,COUNTRY,EMAILID,TOURDESCRIPTIONS,TRAVELDATES,DURATIONOFTHESTAY,NOOFPERSON,CONTACTNO) values(@NAME,@COUNTRY,@EMAILID,@TOURDESCRIPTIONS,@TRAVELDATES,@DURATIONOFTHESTAY,@NOOFPERSON,@CONTACTNO)";
                     MySqlParameter[] insertParams = new MySqlParameter[]
                    {
                        new MySqlParameter("@NAME",rData.addInfo["NAME"]),
                        new MySqlParameter("@COUNTRY",rData.addInfo["COUNTRY"]),
                        new MySqlParameter("@EMAILID",rData.addInfo["EMAILID"]),
                        new MySqlParameter("@TOURDESCRIPTIONS",rData.addInfo["TOURDESCRIPTIONS"]),
                        new MySqlParameter("@TRAVELDATES",rData.addInfo["TRAVELDATES"]),
                        new MySqlParameter("@DURATIONOFTHESTAY",rData.addInfo["DURATIONOFTHESTAY"]),
                        new MySqlParameter("@NOOFPERSON",rData.addInfo["NOOFPERSON"]),
                        new MySqlParameter("@CONTACTNO",rData.addInfo["CONTACTNO"]),
                    };
                    var insertResult = ds.executeSQL(sq, insertParams);

                    resData.rData["rMessage"] = "Successful";
                    
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