using PayloadParserAPI.Services;
using System.Xml;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// parsery
builder.Services.AddScoped<IContentParser, CsvParser>();
builder.Services.AddScoped<IContentParser, JsonParser>();
builder.Services.AddScoped<IDataProcessing, DataProcessing>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();