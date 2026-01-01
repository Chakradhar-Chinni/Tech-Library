var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Configure Memory Cache with size limit and other options //new
builder.Services.AddMemoryCache(options =>
{
    options.SizeLimit = 1024; // Maximum total size in units (not bytes)
    options.CompactionPercentage = 0.25; // Remove 25% of cache when size limit reached
    options.ExpirationScanFrequency = TimeSpan.FromMinutes(1); // Scan for expired items every minute
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
