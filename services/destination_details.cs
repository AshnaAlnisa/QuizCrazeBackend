using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class destination_details
    {
        dbServices ds = new dbServices();
        public async Task<responseData> Destination_details(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
               
                    var sq=@"insert into detailsdb.destination_details(DETAILS) values(@DETAILS)";
                     MySqlParameter[] insertParams = new MySqlParameter[]
                    {
                        new MySqlParameter("@DETAILS",rData.addInfo["DETAILS"]),
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