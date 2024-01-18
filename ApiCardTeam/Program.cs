using ApiCardEmotes;
using Microsoft.AspNetCore.Cors.Infrastructure;
using MongoDB.Driver; // Add this if you are using IMongoClient in UserRepository

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register MongoDB context
builder.Services.AddSingleton<MongoDbContext>(); // This line registers MongoDbContext
builder.Services.AddSingleton<CardService>();   // Assuming CardService uses MongoDbContext

builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    var settings = serviceProvider.GetRequiredService<IConfiguration>();
    var connectionString = settings.GetConnectionString("MongoDb");
    return new MongoClient(connectionString);
});
// Register UserRepository
builder.Services.AddScoped<UserRepository>();   // Add this line to register UserRepository

// Add CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorOrigin",
                      builder =>
                      {
                          builder.WithOrigins("https://localhost:44398")
                                 .AllowAnyHeader()
                                 .AllowAnyMethod();
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowBlazorOrigin");
app.UseAuthorization();

app.MapControllers();

app.Run();
