
<h1> Securing APIs

1. UN & PW auth for every request is bad idea and is vulnerable for attackers
2. Token based Authentication is best 
 - send a token on each request which represents consent
 - validate token at API level


Token Structure:
Payload  - token creation time and some info about user
Signature - hash of payload, used to ensure the data wasn't tampered
Header - Essential token information like the key algorithm used for signing

Token-based security uses signed JWTs containing a header, payload, and signature to authenticate API requests. A login endpoint issues tokens after verifying credentials. Clients must send the token in the Authorization header for access. All API endpoints, except login, require a valid token to ensure secure communication and prevent tampering.




Here's a simple terminal-style diagram using only keyboard symbols to represent the token-based authentication flow:


+------------------+       POST /login       +------------------+
|   Client (App)   | ----------------------> |   API Endpoint   |
| (Postman, App)   |  {username, password}   |   /login         |
+------------------+                         +------------------+
              ^                                        |
              |                                        | Validate credentials
              |                                        v
              |                                +------------------------+
              |                                |   Generate JWT Token   |
              |                                |  (Header, Payload,     |
              |                                |   Signature)           |
              |                                +------------------------+
              |                                        |
              |                                        v
              | <------------------ Token --------- +------------------+
                                                    |   API Endpoint   |
                                                    +------------------+

On Subsequent Requests:
+------------------+     Authorization: Bearer <token>     +------------------+
|   Client (App)   | ------------------------------------> |   Protected API   |
+------------------+                                       |   Endpoints       |
                                                           +------------------+
                                                                  |
                                                                  | Validate Token
                                                                  v
                                                           +------------------+
                                                           |   Access Granted |
                                                           +------------------+








  

  
<h2> Creating a token

Create Authentication Controller.cs
Add Authentication array in appsettings.development.json

## /appsettings.Development.json
{
  "mailSettings": {
    "mailTo": "user@company.in",
    "mailFrom": "dev-admin@company.in"
  },
  "ConnectionStrings": {
    "CityInfoDBConnectionString": "Data Source=CityInfo.db;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "CityInfo.API.Controllers": "Information",
      "Microsoft.EntityFrameworkCore.Database.Command": "Information"
    }
  },
  "Authentication": {
    "SecretForKey": "RgDldLrK+p+T0JisAKdD7THnT/npmWYl4vV3UUiRSVE=",
    "Issuer": "https://localhost:7167",
    "Audience": "cityinfoapi"
  }
}



 ## /Controllers/AuthenticationController.cs
Todo: 2 packages need to be installed here

/*
POST: https://localhost:7167/api/authentication/authenticate
Request Body: 
{
  "Username":"Kevin",
  "Password": "DOckx"
}

Response:
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwiZ2l2ZW5fbmFtZSI6IktldmluIiwiZmFtaWx5X25hbWUiOiJEb2NreCIsImNpdHkiOiJBbnR3ZXJwIiwibmJmIjoxNzUwNDI1MDk2LCJleHAiOjE3NTA0Mjg2OTYsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcxNjciLCJhdWQiOiJjaXR5aW5mb2FwaSJ9.YBqGWP69aE66TfHH1OWFWEPhXzFyIWXlLL0f02GSQpI

Status: 200 OK

Tip: Go to jwt.io and paste this token to check the format

*/
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public class AuthenticationRequestBody
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        private class CityInfoUser
        {
            public int UserId { get; }
            public string UserName { get; }
            public string FirstName { get; }
            public string LastName { get; }
            public string City { get; }

            public CityInfoUser(int userId, string userName, string firstName, string lastName, string city)
            {
                UserId = userId;
                UserName = userName;
                FirstName = firstName;
                LastName = lastName;
                City = city;
            }
        }

        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate( AuthenticationRequestBody authenticationRequestBody)
        {

            //Step1: Validate username, password
            var user = ValidateUserCredentials(authenticationRequestBody.UserName, authenticationRequestBody.Password);

            if (user == null)
            {
                return Unauthorized();
            }


            //step2: create a token
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //the Claims that
            var claimsForToken = new List<Claim>();
            
                claimsForToken.Add(new Claim("sub", user.UserId.ToString()));
                claimsForToken.Add(new Claim("given_name", user.FirstName));
                claimsForToken.Add(new Claim("family_name", user.LastName));
                claimsForToken.Add(new Claim("city", user.City));
            

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials);

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return Ok(tokenToReturn);
        }

        private CityInfoUser ValidateUserCredentials(string? userName, string? password)
        {
            // In real scenarios, validate against a database
            return new CityInfoUser(1, userName?? "", "Kevin", "Dockx", "Antwerp");
        }
    }
}













<h2> Validating a token, add authentication middleware and [Authorize] every Controller

/*
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
POST: https://localhost:7167/api/authentication/authenticate
Request Body: 
{
  "Username":"Kevin",
  "Password": "DOckx"
}

Response:
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwiZ2l2ZW5fbmFtZSI6IktldmluIiwiZmFtaWx5X25hbWUiOiJEb2NreCIsImNpdHkiOiJBbnR3ZXJwIiwibmJmIjoxNzUwNDI1MDk2LCJleHAiOjE3NTA0Mjg2OTYsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcxNjciLCJhdWQiOiJjaXR5aW5mb2FwaSJ9.YBqGWP69aE66TfHH1OWFWEPhXzFyIWXlLL0f02GSQpI

Status: 200 OK
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

GET: https://localhost:7167/api/cities
Authorization: None

Status: 401 Unauthorized
Response:

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
GET: https://localhost:7167/api/cities
Authorization: None

Status: 200 OK
Response:
[
  {
    "id": 3,
    "name": "Chicago",
    "description": "The Windy City"
  },
  {
    "id": 2,
    "name": "Los Angeles",
    "description": "The City of Angels"
  },
  {
    "id": 1,
    "name": "New York City",
    "description": "The Big Apple"
  }
]
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
Note: If a 2nd token is generated before 1st token expires, then both tokens are valid. To avoid previous token when new tokens are generated implement RefreshTokens mechanism

*/



1. Program.cs
    Add bearer authentication, UseAuthentication() middleware 
2. for every controller, add [Authorize] with using Microsoft.AspNetCore.Authorization

## Nuget Packages
Install Microsoft.AspNetCore.Authentication.JwtBearer v8.0.0


  
## Program.cs
using CityInfo.API;
using CityInfo.API.DbContexts;
using CityInfo.API.Services;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens.Experimental;
using Serilog;
using System.Text;
//using Microsoft.AspNetCore.Authentication.JwtBearer;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/cityinfo.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();
builder.Host.UseSerilog();


//builder.Host.UseSerilog(Log.Logger);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true; //return 406 if the client requests a format that is not supported
}).AddNewtonsoftJson()
   .AddXmlDataContractSerializerFormatters();

builder.Services.AddProblemDetails();

// Send Additional error messages
//builder.Services.AddProblemDetails(options =>
//{
//    options.CustomizeProblemDetails = ctx =>
//    {
//        ctx.ProblemDetails.Instance = ctx.HttpContext.Request.Path;
//        ctx.ProblemDetails.Title = "Custom Error";
//        ctx.ProblemDetails.Detail = "Custom error message";
//        ctx.ProblemDetails.Extensions.Add("Process Id", Environment.ProcessId);
//    };
//});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer(); //endpoint discovery
builder.Services.AddSwaggerGen(); //integrates Swagger
builder.Services.AddSingleton<FileExtensionContentTypeProvider>(); //FileExtensionContentTypeProvider is used to get the content type of a file based on its extension

builder.Services.AddDbContext<CityInfoContext>(dbContextOptions 
    => dbContextOptions.UseSqlite(builder.Configuration["ConnectionStrings:CityInfoDBConnectionString"]));

builder.Services.AddSingleton<CitiesDataStore>();
builder.Services.AddScoped<ICityInfoRepository, CityInfoRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAuthentication("Bearer") //added
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new()// TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            //IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(builder.Configuration["Authentication:SecretForKey"]))
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
        };
    } 
    );

#if DEBUG
builder.Services.AddTransient<ILocalMailService, LocalMailService>();
#else
builder.Services.AddTransient<ILocalMailService, CloudMailService>();
#endif

var app = builder.Build(); // Build the app



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
   // app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler();
}

app.UseRouting();
app.UseHttpsRedirection(); //Http are re-directed to Https


app.UseAuthentication(); //added

app.UseAuthorization();

app.MapControllers(); //Maps Http Requests to controllers

app.UseEndpoints(endpoints =>
  {
      endpoints.MapControllers();
  });

//app.Run(async (context) =>
//{
//    await context.Response.WriteAsync("Hello World!");
//});

app.Run();




## Authorize every Controller
using Microsoft.AspNetCore.Mvc;
using CityInfo.API.Models;
using CityInfo.API.Services;
using AutoMapper;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;


namespace CityInfo.API.Controllers
{
    [ApiController]
    //[Route("api/cities")]
    [Authorize]
    [Route("api/[controller]")]
}

  







<h2> Using Information from the token in Controller
