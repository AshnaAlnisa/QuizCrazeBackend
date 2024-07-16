
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
