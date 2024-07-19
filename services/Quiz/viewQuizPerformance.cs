// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using MySql.Data.MySqlClient;

// namespace COMMON_PROJECT_STRUCTURE_API.services
// {
//     public class viewQuizPerformance
//     {
//         dbServices ds = new dbServices();

//         public async Task<responseData> ViewQuizPerformance(requestData rData)
//         {
//             responseData resData = new responseData();

//             try
//             {
//                 // Assuming 'id' corresponds to the email of the user to fetch results for
                

//                 var query = @"
//                     SELECT
//                         q.id AS quiz_id,
//                         q.title AS quiz_title,
//                         COUNT(r.id) AS times_played
//                     FROM
//                         quizzes q
//                     LEFT JOIN
//                         result r ON q.id = r.quiz_id
//                     JOIN
//                         users u ON r.user_id = u.id
//                     WHERE
//                         u.email = @UserEmail
//                     GROUP BY
//                         q.id, q.title
//                     ORDER BY
//                         times_played DESC";

//                 // MySqlParameter[] myParams = new MySqlParameter[]
//                 // {
//                 //     new MySqlParameter("@email", rData.addInfo["email"])
//                 // };

//                 var dbData = ds.executeSQL(query, null);

//                 List<object> itemsList = new List<object>();

//                 foreach (var row in dbData)
//                 {
//                     // Construct item object
//                     var item = new
//                     {
//                         quiz_id = Convert.ToInt32(row["quiz_id"]),
//                         quiz_title = row["quiz_title"].ToString(),
//                         times_played = Convert.ToInt32(row["times_played"])
//                     };

//                     itemsList.Add(item);
//                 }

//                 resData.rData["items"] = itemsList;
//                 resData.rData["rMessage"] = "Successful";
//             }
//             catch (Exception ex)
//             {
//                 resData.rData["rMessage"] = "Exception occurred: " + ex.Message;
//             }

//             return resData;
//         }
//     }
// }
