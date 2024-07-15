using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;


public class logout
{
    private readonly dbServices ds = new dbServices();
       
        private readonly string secretKey = "vami czfo yhis ykpr"; // Define your secret key for JWT

     public async Task<responseData> Logout(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var token = rData.addInfo["TOKEN"].ToString();
                if (string.IsNullOrEmpty(token))
                {
                    throw new ArgumentException("Token is missing");
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(secretKey);

                // Validate the JWT token
                try
                {
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                    // Token is valid
                    resData.rData["rMessage"] = "Logout successful";
                }
                catch (Exception ex)
                {
                    // Token is invalid or expired
                    resData.rData["rMessage"] = "Invalid or expired token";
                    Console.WriteLine($"Exception occurred while validating token: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                // General error handling
                resData.rData["rMessage"] = "Error: " + ex.Message;
                Console.WriteLine($"Exception occurred in Logout method: {ex.Message}");
            }
            return resData;
        }


}
