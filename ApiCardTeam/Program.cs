using ApiCardEmotes;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Identity.Web; // Added this line

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register MongoDB context
builder.Services.AddSingleton<MongoDbContext>();  // This line registers MongoDbContext
builder.Services.AddSingleton<CardService>();    // Assuming CardService uses MongoDbContext

// Configure Microsoft Identity
builder.Services.AddMicrosoftIdentityWebApiAuthentication(builder.Configuration, "AzureAd");

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();