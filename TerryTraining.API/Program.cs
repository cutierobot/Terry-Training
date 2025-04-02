using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TerryTraining.Application.Interfaces;
using TerryTraining.Application.Services;
using TerryTraining.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
    {
        // using System.Reflection;
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        options.SupportNonNullableReferenceTypes();
    }
);

var app = builder.Build();


// TODO: don't store hardcoded password and stuff here in OrderDatabase connectionString be more secure
var connectionString =
    builder.Configuration.GetConnectionString("OrderDatabase")
    ?? throw new InvalidOperationException("Connection string" + "'OrderDatabase' not found.");

// https://medium.com/swlh/creating-a-multi-project-net-core-database-solution-a69decdf8d7e
// this might be how we are to add it
builder.Services.AddDbContext<TerryDbContext>(
    options =>
        options.UseSqlServer(connectionString)
);

builder.Services.AddScoped<ITerryTrainingService, TerryTrainingService>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/quote/find{text}", async (string text, ITerryTrainingService terryTrainingService) =>
    {
        throw new NotImplementedException();
    })
    .WithName("FindQuotes")
    .WithTags("Not Implemented")
    .WithOpenApi(x => new OpenApiOperation(x)
    {
        Summary = "Find quotes matching provided text",
        Description = "Retrieves a array of quotes matching provided text"
    });

app.Run();
