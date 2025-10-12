using Core.Features.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<ISingletonService, SingletonService>();
builder.Services.AddScoped<IScopedService, ScopedService>();
builder.Services.AddTransient<ITransientService,TransientService>();

var app = builder.Build();

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


/*
 * Controllers (scoped) are created per request and disposed after each request.
 * Singleton services live throughout the app lifetime and are only disposed when the app shuts down.
 * if IDisposable is not implemented manually, GC takes care of it as its managed memory
 * 
 * Check: use IDisposable to release unmanaged resources.
 * cont: 3types in same app - refer chatgpt GUID exciting & fresh
 */
