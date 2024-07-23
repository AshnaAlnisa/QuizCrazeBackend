using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class myLogin
    {
        private readonly dbServices ds = new dbServices();
        private readonly string secretKey = "vami czfo yhis ykpr"; // Define your secret key for JWT

        // Define default admin credentials
        private const string DefaultAdminEmail = "admin@example.com";
        private const string DefaultAdminPassword = "admin123";
        private const bool DefaultAdminStatus = true; // You can use this flag to determine admin status

        public async Task<responseData> MyLogin(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var email = rData.addInfo["email"].ToString();
                var password = rData.addInfo["password"].ToString();

                // Check if the provided credentials match admin credentials
                bool isAdmin = (email == DefaultAdminEmail && password == DefaultAdminPassword);

                var query = @"SELECT * FROM quizcraze.users WHERE email=@email AND password=@password";
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@email", email),
                    new MySqlParameter("@password", password),
                };

                var dbData = ds.executeSQL(query, myParam);
                if (dbData[0].Any())
                {
                    // Generate JWT token and return it
                    var token = GenerateToken(email, isAdmin); // Pass isAdmin flag to GenerateToken
                    resData.rData["rMessage"] = "Signin Successful";
                    resData.rData["TOKEN"] = token;
                    resData.rData["email"] = email;
                    resData.rData["isAdmin"] = isAdmin; // Optionally return isAdmin status
                }
                else
                {
                    resData.rData["rMessage"] = "Invalid email or password";
                }

            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "Error: " + ex.Message;
            }
            return resData;
        }

        private string GenerateToken(string email, bool isAdmin)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, isAdmin ? "admin" : "user") // Assign role claim based on isAdmin
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Token expires in 1 hour
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
