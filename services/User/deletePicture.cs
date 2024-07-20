using MySql.Data.MySqlClient;


public class deletePicture
{
    private dbServices ds = new dbServices();

    public async Task<responseData> DeletePicture(requestData rData)
    {
        responseData resData = new responseData();

        try
        {
            // Your delete query to remove the profile picture entry
           var query = @"UPDATE quizcraze.users 
              SET picture = NULL 
              WHERE user_id = @user_id;";


            // Your parameters
            MySqlParameter[] myParam = new MySqlParameter[]
            {
                new MySqlParameter("@user_id", rData.addInfo["user_id"])
            };

            // Condition to execute the delete query
            bool shouldExecuteDelete = true;

            if (shouldExecuteDelete)
            {
                int rowsAffected = ds.ExecuteUpdateSQL(query, myParam); // Assuming ExecuteUpdateSQL can handle DELETE queries

                if (rowsAffected > 0)
                {
                    resData.rData["rMessage"] = "Profile picture deleted successfully.";
                }
                else
                {
                    resData.rData["rMessage"] = "No rows affected. Deletion failed.";
                }
            }
            else
            {
                resData.rData["rMessage"] = "Condition not met. Delete query not executed.";
            }
        }
        catch (Exception ex)
        {
            resData.rData["rMessage"] = "Exception occurred: " + ex.Message;
        }
        return resData;
    }
}
