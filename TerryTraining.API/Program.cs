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
builder.Services.AddSwaggerGen();
// builder.Services.AddSwaggerGen(options =>
//     {
//         // using System.Reflection;
//         var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
//         options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
//         options.SupportNonNullableReferenceTypes();
//     }
// );


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


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.MapGet("/quote/find{text}", async (string text, ITerryTrainingService terryTrainingService) =>
//     {
//         throw new NotImplementedException();
//     })
//     .WithName("FindQuotes")
//     .WithTags("Not Implemented")
//     .WithOpenApi(x => new OpenApiOperation(x)
//     {
//         Summary = "Find quotes matching provided text",
//         Description = "Retrieves a array of quotes matching provided text"
//     });

app.MapPut("/product/new", async (string name, string description, int stockcount, ITerryTrainingService terryTrainingService) =>
    {
        var result = terryTrainingService.NewProduct(name, description, stockcount);
        // throw new NotImplementedException();
    })
    .WithName("NewProduct")
    .WithTags("Not Implemented")
    .WithOpenApi(x => new OpenApiOperation(x)
    {
        Summary = "Create a new product",
        Description = "Creates a new product, if it does not exist",
    });

app.MapGet("/product/{id}", async (int id, ITerryTrainingService terryTrainingService) =>
    {
        var result = await terryTrainingService.GetProduct(id);
        return result == null ? Results.NotFound(): Results.Ok(result);
    })
    .WithName("GetProduct")
    .WithOpenApi(x => new OpenApiOperation(x)
    {
        Summary = "Retrieve a product from it's Id",
        Description = "Retrieves a product using it's db Id, this one is intended for testing connection if anything"
    });

// Check doesn’t already exist
// Check name, description sizes
// Check stockcount > 0


app.Run();
