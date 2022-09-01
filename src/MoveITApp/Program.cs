using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MoveITApp.Helpers;
using MoveITApp.Shared.AppSettings;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//read from appSettings.json, find the section DbSettings 
var dbSettingsConfig = builder.Configuration.GetSection("DbSettings");
builder.Services.Configure<DbSettings>(dbSettingsConfig);
DbSettings dbSettings = dbSettingsConfig.Get<DbSettings>();

DependencyInjectionHelper.InjectDbContext(builder.Services, dbSettings.ConnectionString);
DependencyInjectionHelper.InjectRepositories(builder.Services);
DependencyInjectionHelper.InjectServices(builder.Services);

//Configure JWT
//read from appSettings.json, find the section AuthenticationSettings 
var authenticationSettingsConfig = builder.Configuration.GetSection("AuthenticationSettings");
builder.Services.Configure<AuthenticationSettings>(authenticationSettingsConfig);
AuthenticationSettings authenticationSettings = authenticationSettingsConfig.Get<AuthenticationSettings>();

builder.Services.AddAuthentication(x =>
{
    //we will use JWT authentication
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authenticationSettings.JWTSecret))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
