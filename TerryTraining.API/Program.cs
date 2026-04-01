using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TerryTraining.API.Validation;
using TerryTraining.Application.DTO;
using TerryTraining.Application.Interfaces;
using TerryTraining.Application.Services;
using TerryTraining.Domain.Interfaces;
using TerryTraining.Persistence;
using TerryTraining.Persistence.Repository;

var builder = WebApplication.CreateBuilder(args);

/*
 *---------------------------
 * TODO/WHAT I DID LAST TIME
 *---------------------------
 * Previously learnt about Aggreggates.
 * figured out Order and OrderLines are. A customer creates one single order, that order will have multiple
 * OrderLine's (one for each product in the Order)
 * Learned about ValueObjects. Basically a thing where they don't need to be unique as the data is unique enough. such as a address.
 * Can be created with either record of base class
 *
 *---------------------------
 * TODO NEXT WEEK/WHERE IM UP DO
 *---------------------------
 * Start creating an writing code for Order Aggregate using knowledge of valueObject and Aggregates. Ask Terry
 * if should use record or base class
 */

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

builder.Services.AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters()
    .AddValidatorsFromAssemblyContaining<ProductValidator>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.MapGet("/quote/find{text}", async (string text, IProductService productService) =>
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

app.MapPut("/product/new", async (string name, string description, int stockcount, IProductService productService) =>
    {
        // create ProductDTO here to send to NewProduct
        var product = new ProductDTO
            {
                Description = description,
                Name = name,
                Stock = stockcount,
                Reserved = 0 // default of DB is 0 anyway, but still force sending it as best practice
            };
        
        var result = await productService.NewProduct(product);
        
         return result == null ? Results.NotFound() : Results.Created($"/product/{result.Value.Id}", result);
    })
    .WithName("NewProduct")
    .WithOpenApi(x => new OpenApiOperation(x)
    {
        Summary = "Create a new product",
        Description = "Creates a new product, if it does not exist",
    });

app.MapPut("/product/{productid}/stock", async (int productid, int stockcount, IProductService productService) =>
    {
        // create ProductDTO here to send to NewProduct
        var product = new ProductDTO
            {
                Id = productid,
                Stock = stockcount,
            };
        
        var result = productService.AddStock(product);
         return result == null ? Results.NotFound() : Results.Created($"/product/{result.Id}", result);
    })
    .WithName("AddStock")
    .WithTags("Not Implemented")
    .WithOpenApi(x => new OpenApiOperation(x)
    {
        Summary = "Adds stock to a product",
        Description = "Adds stock to a product with provided Id",
    });

// CreateCustomer(givenName, surname, addressDetails)
app.MapPut("/customer/new", async (string givenName, string surname, string addressDetails/*, IProductService productService*/) =>
    {
      // 1. create a customerDTO
      // 2. create a customerService
      // 3. add to unit of work.???
    })
    .WithName("CreateCustomer")
    .WithTags("Not Implemented")
    .WithOpenApi(x => new OpenApiOperation(x)
    {
        Summary = "Creates a customer",
        Description = "Creates a customer from name and address details",
    });

// CreateOrder(customerId, orderLines)
// ask Terry about this one what orderLines is
app.MapPut("/order/new", async (int customerId/*, IProductService productService*/) =>
    {
        // Products exist
        // Ensure customer exists
        // Ensure stock available
        // Reserve stock
    })
    .WithName("CreateOrder")
    .WithTags("Not Implemented")
    .WithOpenApi(x => new OpenApiOperation(x)
    {
        Summary = "Creates a order",
        Description = "Not really sure on this one to ask Terry about",
    });


// FullfillOrder(orderId)
app.MapPut("/order/update/{orderId}", async (int orderId/*, IOrderService orderService*/) =>
    {
        // Ensure order exists
        // Update stock levels and reserve levels
        // Set order completed
    })
    .WithName("FullfillOrder")
    .WithTags("Not Implemented")
    .WithOpenApi(x => new OpenApiOperation(x)
    {
        Summary = "Updates a Order",
        Description = "Updates a Order ensuring stock availability, stocks levels updated after order and that if order can be completed successfully it is marked as such",
    });


// GetCustomer(id)
app.MapGet("/customer/{id}", async (int id/*, IProductService productService*/) =>
    {
        // 1.
    })
    .WithName("GetCustomer")
    .WithTags("Not Implemented")
    .WithOpenApi(x => new OpenApiOperation(x)
    {
        Summary = "Retrieve a customer by their Id",
        Description = "Retrieves a customer using it's unique Id"
    });

// GetOrder(orderId)
app.MapGet("/order/{orderId}", async (int orderId/*, IOrderService orderService*/) =>
    {
        // Ensure order exists
        // Return all details except customer details
    })
    .WithName("GetOrder")
    .WithTags("Not Implemented")
    .WithOpenApi(x => new OpenApiOperation(x)
    {
        Summary = "Retrieve a order by it's Id",
        Description = "Retrieves a Order places using it's unique Id"
    });

app.MapGet("/product/{id}", async (int id, IProductService productService) =>
    {
        var result = await productService.GetProduct(id);
        return result == null ? Results.NotFound(): Results.Ok(result);
    })
    .WithName("GetProduct")
    .WithOpenApi(x => new OpenApiOperation(x)
    {
        Summary = "Retrieve a product from it's Id",
        Description = "Retrieves a product using it's db Id, this one is intended for testing connection if anything"
    });

app.Run();
