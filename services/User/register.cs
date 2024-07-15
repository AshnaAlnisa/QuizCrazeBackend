using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System;
using System.Collections;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class register
    {
        dbServices ds = new dbServices();

        public async Task<responseData> Register(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"SELECT * FROM quizcraze.users WHERE email=@email";
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@email", rData.addInfo["email"])
                };
                var dbData = ds.executeSQL(query, myParam);

                if (dbData[0].Count() > 0)
                {
                    resData.rData["rMessage"] = "Duplicate Credentials";
                }
                else
                {
                    var sq = @"INSERT INTO quizcraze.users (username, name, email, password, address, picture) 
                               VALUES (@username, @name, @email, @password, @address, @picture)";
                    MySqlParameter[] insertParams = new MySqlParameter[]
                    {
                        new MySqlParameter("@username", rData.addInfo["username"]),
                        new MySqlParameter("@name", rData.addInfo["name"]),
                        new MySqlParameter("@email", rData.addInfo["email"]),
                        new MySqlParameter("@password", rData.addInfo["password"]),
                        new MySqlParameter("@address", rData.addInfo["address"]),
                        new MySqlParameter("@picture", rData.addInfo["picture"])
                    };
                    var insertResult = ds.executeSQL(sq, insertParams);

                    resData.rData["rMessage"] = "Signup Successful";
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
