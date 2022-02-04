using AutoMapper;
using DigitalSolutions.TestApp.WebAPI;
using DigitalSolutions.TestApp.WebAPI.DataBase;

// Create builder
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IData>(service => new DataCsvController(@"D:\Programming\Testing\Invoices.csv"));

var mapperConfig = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Create app
var app = builder.Build();

app.UseAuthorization();

app.UseCors(cors => cors.AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowAnyOrigin());

app.MapControllers();

app.Run();
