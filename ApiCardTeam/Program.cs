using ApiCardEmotes;
using Microsoft.AspNetCore.Cors.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddSingleton<CardService>();


//// Register MongoDB context
//builder.Services.AddSingleton<MongoDbContext>(sp =>
//{
//    var configuration = sp.GetService<IConfiguration>();
//    return new MongoDbContext(configuration);
//});

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
