
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using COMMON_PROJECT_STRUCTURE_API.services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authorization;


WebHost.CreateDefaultBuilder().
ConfigureServices(s=>
{
    IConfiguration appsettings = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
    s.AddSingleton<login>();
    s.AddSingleton<register>();
    s.AddSingleton<myLogin>();
    s.AddSingleton<updateUser>();
    s.AddSingleton<updatePicture>();
    s.AddSingleton<deletePicture>();
    s.AddSingleton<changePassword>();
    s.AddSingleton<insertContactUs>();
    s.AddSingleton<viewContactUs>();
    s.AddSingleton<deleteContactUs>();
    s.AddSingleton<viewUsers>();
    s.AddSingleton<deleteUsers>();
    s.AddSingleton<logout>();
    s.AddSingleton<insertQuiz>();
    s.AddSingleton<updateQuiz>();
    s.AddSingleton<viewQuiz>();
    s.AddSingleton<deleteQuiz>();
    s.AddSingleton<insertResult>();
    s.AddSingleton<viewResult>();
    s.AddSingleton<deleteCardQuiz>();
    s.AddSingleton<insertCardQuiz>();
    s.AddSingleton<updateCardQuiz>();
    s.AddSingleton<viewCardQuiz>();
    s.AddSingleton<viewLeaderboard>();
    s.AddSingleton<viewQuizPerformance>();
    s.AddSingleton<viewUserActivity>();



s.AddAuthorization();
s.AddControllers();
s.AddCors();
s.AddAuthentication("SourceJWT").AddScheme<SourceJwtAuthenticationSchemeOptions, SourceJwtAuthenticationHandler>("SourceJWT", options =>
    {
        options.SecretKey = appsettings["jwt_config:Key"].ToString();
        options.ValidIssuer = appsettings["jwt_config:Issuer"].ToString();
        options.ValidAudience = appsettings["jwt_config:Audience"].ToString();
        options.Subject = appsettings["jwt_config:Subject"].ToString();
    });
}).Configure(app=>
{
app.UseAuthentication();
app.UseAuthorization();
 app.UseCors(options =>
         options.WithOrigins("https://localhost:5002", "http://localhost:5001")
         .AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseRouting();
app.UseStaticFiles();

app.UseEndpoints(e=>
{
           var login=  e.ServiceProvider.GetRequiredService<login>();
           var register=  e.ServiceProvider.GetRequiredService<register>();
           var myLogin=  e.ServiceProvider.GetRequiredService<myLogin>();
           var updateUser=  e.ServiceProvider.GetRequiredService<updateUser>();
           var updatePicture=  e.ServiceProvider.GetRequiredService<updatePicture>();
           var deletePicture=  e.ServiceProvider.GetRequiredService<deletePicture>();
           var changePassword=  e.ServiceProvider.GetRequiredService<changePassword>();
           var insertContactUs=  e.ServiceProvider.GetRequiredService<insertContactUs>();
           var viewContactUs=  e.ServiceProvider.GetRequiredService<viewContactUs>();
           var deleteContactUs=  e.ServiceProvider.GetRequiredService<deleteContactUs>();
           var viewUsers=  e.ServiceProvider.GetRequiredService<viewUsers>();
           var deleteUsers=  e.ServiceProvider.GetRequiredService<deleteUsers>();
           var logout=  e.ServiceProvider.GetRequiredService<logout>();
           var insertQuiz=  e.ServiceProvider.GetRequiredService<insertQuiz>();
           var updateQuiz=  e.ServiceProvider.GetRequiredService<updateQuiz>();
           var viewQuiz=  e.ServiceProvider.GetRequiredService<viewQuiz>();
           var deleteQuiz=  e.ServiceProvider.GetRequiredService<deleteQuiz>();
           var insertResult=  e.ServiceProvider.GetRequiredService<insertResult>();
           var viewResult=  e.ServiceProvider.GetRequiredService<viewResult>();
           var deleteCardQuiz=  e.ServiceProvider.GetRequiredService<deleteCardQuiz>();
           var insertCardQuiz=  e.ServiceProvider.GetRequiredService<insertCardQuiz>();
           var updateCardQuiz=  e.ServiceProvider.GetRequiredService<updateCardQuiz>();
           var viewCardQuiz=  e.ServiceProvider.GetRequiredService<viewCardQuiz>();
           var viewLeaderboard=  e.ServiceProvider.GetRequiredService<viewLeaderboard>();
           var viewQuizPerformance=  e.ServiceProvider.GetRequiredService<viewQuizPerformance>();
           var viewUserActivity=  e.ServiceProvider.GetRequiredService<viewUserActivity>();


           


 e.MapPost("login",
         [AllowAnonymous] async (HttpContext http) =>
         {
             var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
             requestData rData = JsonSerializer.Deserialize<requestData>(body);
              if (rData.eventID == "1001") // update
                         await http.Response.WriteAsJsonAsync(await login.Login(rData));

         });
        e.MapPost("register",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await register.Register(rData));
            });
        e.MapPost("myLogin",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await myLogin.MyLogin(rData));
            });
        e.MapPost("updateUser",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await updateUser.UpdateUser(rData));
            });
        e.MapPost("updatePicture",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await updatePicture.UpdatePicture(rData));
            });
        e.MapPost("deletePicture",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await deletePicture.DeletePicture(rData));
            });
        e.MapPost("changePassword",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await changePassword.ChangePassword(rData));
            });
        e.MapPost("insertContactUs",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await insertContactUs.InsertContactUs(rData));
            });
        e.MapPost("viewContactUs",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await viewContactUs.ViewContactUs(rData));
            });
        e.MapPost("deleteContactUs",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await deleteContactUs.DeleteContactUs(rData));
            });
        e.MapPost("viewUsers",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await viewUsers.ViewUsers(rData));
            });
         
        e.MapPost("deleteUsers",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await deleteUsers.DeleteUsers(rData));
            });
        e.MapPost("logout",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await logout.Logout(rData));
            });
        e.MapPost("insertQuiz",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await insertQuiz.InsertQuiz(rData));
            });
        e.MapPost("updateQuiz",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await updateQuiz.UpdateQuiz(rData));
            });
        e.MapPost("viewQuiz",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await viewQuiz.ViewQuiz(rData));
            });
        e.MapPost("deleteQuiz",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await deleteQuiz.DeleteQuiz(rData));
            });
        e.MapPost("insertResult",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await insertResult.InsertResult(rData));
            });
        e.MapPost("viewResult",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await viewResult.ViewResult(rData));
            });
        e.MapPost("deleteCardQuiz",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await deleteCardQuiz.DeleteCardQuiz(rData));
            });
        e.MapPost("insertCardQuiz",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await insertCardQuiz.InsertCardQuiz(rData));
            });
        e.MapPost("updateCardQuiz",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await updateCardQuiz.UpdateCardQuiz(rData));
            });
        e.MapPost("viewCardQuiz",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await viewCardQuiz.ViewCardQuiz(rData));
            });
        e.MapPost("viewLeaderboard",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await viewLeaderboard.ViewLeaderboard(rData));
            });
        e.MapPost("viewQuizPerformance",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await viewQuizPerformance.ViewQuizPerformance(rData));
            });
        e.MapPost("viewUserActivity",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await viewUserActivity.ViewUserActivity(rData));
            });
         



         IConfiguration appsettings = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
 e.MapGet("/dbstring",
               async c =>
               {
                   dbServices dspoly = new dbServices();
                   await c.Response.WriteAsJsonAsync("{'mongoDatabase':" + appsettings["mongodb:connStr"] + "," + " " + "MYSQLDatabase" + " =>" + appsettings["db:connStrPrimary"]);
               });
          e.MapGet("/bing",
                async c => await c.Response.WriteAsJsonAsync("{'Name':'Anish','Age':'26','Project':'COMMON_PROJECT_STRUCTURE_API'}"));
});
}).Build().Run();
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
public record requestData
{
    [Required]
    public string eventID { get; set; }
    [Required]
    public IDictionary<string, object> addInfo { get; set; }
}

public record responseData
{
    public responseData()
    {
        eventID = "";
        rStatus = 0;
        rData = new Dictionary<string, object>();
    }
    [Required]
    public int rStatus { get; set; } = 0;
    public string eventID { get; set; }
    public IDictionary<string, object> addInfo { get; set; }
    public IDictionary<string, object> rData { get; set; }
}
