
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
    public class Authentication
    {
        private readonly dbServices ds = new dbServices();
        private readonly Dictionary<string, int> otpStorage = new Dictionary<string, int>();

        public async Task<responseData> GenerateOTP(string email)
        {
            responseData resData = new responseData();
             try
            {
                // Generate a random OTP
                Random random = new Random();
                int otp = random.Next(100000, 999999);

                // Store the OTP with the email
                otpStorage[email] = otp;

                // Send the OTP to the user's email address
                await SendOTPEmail(email, otp);

                resData.rData["rMessage"] = $"OTP generated successfully and sent to {email}";
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "Error: " + ex.Message;
            }
            return resData;
        }
     private async Task SendOTPEmail(string email, int otp)
{
    try
    {
        string senderEmail = "ashnaalnisa6@gmail.com@gmail.com"; // Your Gmail address
        string senderPassword = "vami czfo yhis ykpr"; // Use your app-specific password for Gmail

        MailMessage mail = new MailMessage();
        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");

        // Configure email message
        mail.From = new MailAddress(senderEmail);
        mail.To.Add(email);
        mail.Subject = "OTP for Password Reset";
        mail.Body = $"Your OTP for password reset is: {otp}";

        // Configure SMTP server
        smtpServer.Port = 587; // Gmail SMTP port for TLS/STARTTLS
        smtpServer.Credentials = new NetworkCredential(senderEmail, senderPassword);

        // Enable SSL/TLS
        smtpServer.EnableSsl = true;

        // Send email asynchronously
        await smtpServer.SendMailAsync(mail);
    }
    catch (Exception ex)
    {
        throw new Exception("Error sending email: " + ex.Message);
    }
}
        public async Task<responseData> VerifyOTP(string email, int otp)
        {
            responseData resData = new responseData();
            try
            {
                // Verify the OTP
                if (otpStorage.ContainsKey(email) && otpStorage[email] == otp)
                {
                    otpStorage.Remove(email);
                    resData.rData["rMessage"] = "OTP verified successfully";
                }
                else
                {
                    resData.rData["rMessage"] = "Invalid OTP";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "Error: " + ex.Message;
            }
            return resData;
        }

        public async Task<responseData> HandleAuthentication(requestData rData)
        {
            var action = rData.addInfo["action"].ToString();
            var email = rData.addInfo["EMAIL"].ToString();

            if (action == "forgotpassword")
            {
                return await GenerateOTP(email);
            }
            else if (action == "verifyotp")
            {
                int otp = int.Parse(rData.addInfo["OTP"].ToString());
                return await VerifyOTP(email, otp);
            }
            else
            {
                return new responseData
                {
                    rData = new Dictionary<string, object>
                    {
                        ["rMessage"] = "Invalid action"
                    }
                };
            }
        }
    }
}
