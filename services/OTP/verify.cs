using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Text.Json;
using System.Net;
using System.Net.Mail;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
   public class verify
{
    public  dbServices ds = new dbServices();
    public async Task<responseData> Verifyotp(requestData rData)
        {
            responseData resData = new responseData();

            string connectionString = "server=127.0.0.1;user=root;password=root;port=3306;database=detailsdb; Max Pool Size=200;";

            try
            {
                string email = rData.addInfo["EMAILID"].ToString();
                string otp = rData.addInfo["OTP"].ToString();

                // Validate OTP
                var validateOTPQuery = @"SELECT COUNT(*) FROM detailsdb.otp_table WHERE EMAILID=@EMAILID AND OTP=@OTP";
                using (var connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (var cmd = new MySqlCommand(validateOTPQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@EMAILID", email);
                        cmd.Parameters.AddWithValue("@OTP", otp);

                        object countObj = await cmd.ExecuteScalarAsync();

                        if (countObj != null && countObj != DBNull.Value)
                        {
                            if (int.TryParse(countObj.ToString(), out int count) && count > 0)
                            {
                                // Delete used OTP from OTP4_USER table
                                var deleteOTPQuery = @"DELETE FROM detailsdb.otp_table WHERE EMAILID=@EMAILID AND OTP=@OTP";
                                using (var deleteCmd = new MySqlCommand(deleteOTPQuery, connection))
                                {
                                    deleteCmd.Parameters.AddWithValue("@EMAILID", email);
                                    deleteCmd.Parameters.AddWithValue("@OTP", otp);
                                    await deleteCmd.ExecuteNonQueryAsync();
                                }

                                resData.rData["success"] = true;
                                resData.rData["message"] = "OTP verified successfully.";
                            }
                            else
                            {
                                resData.rData["message"] = "Invalid OTP.";
                            }
                        }
                        else
                        {
                            resData.rData["message"] = "Invalid OTP.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resData.rData["message"] = "Exception occurred: " + ex.Message;
                Console.WriteLine("Exception in VerifyOTP: " + ex.Message);
            }

            return resData;
        }

    }
}