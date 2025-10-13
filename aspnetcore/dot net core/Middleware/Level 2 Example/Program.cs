using Core.Features.Services;
using Core.Features.Middlewares;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

/*
 * COnfigure Custom Middlewares
 */
app.UseMiddleware<Core.Features.Middlewares.LoggingMiddleware>();
app.UseMiddleware<Core.Features.Middlewares.ExceptionHandlingMiddleware>();
app.UseMiddleware<Core.Features.Middlewares.RequestTimingMiddleware>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
