using Core.Features.Services;
using Core.Features.Middlewares;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*
 * DI registration- one instance for the entire application
*/
builder.Services.AddSingleton<ISingletonService, SingletonService>();
builder.Services.AddScoped<IScopedService, ScopedService>();
builder.Services.AddTransient<ITransientService,TransientService>();

var app = builder.Build();

/*
 * COnfigure Custom Middlewares
 */
app.UseMiddleware<Core.Features.Middlewares.FirstMiddleware>();
app.UseMiddleware<Core.Features.Middlewares.SecondMiddleware>();
app.UseMiddleware<Core.Features.Middlewares.ThirdMiddleware>();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
