using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TerryTraining.API.Validation;
using TerryTraining.Application.DTO;
using TerryTraining.Application.Interfaces;
using TerryTraining.Application.Services;
using TerryTraining.Domain.Entities.OrderAggregate;
using TerryTraining.Domain.Interfaces;
using TerryTraining.Persistence;
using TerryTraining.Persistence.Repository;

var builder = WebApplication.CreateBuilder(args);

/*
 *---------------------------
 * TODO/WHAT I DID LAST TIME
 *---------------------------
 * 95% sure finished the OrderAggregate part just need to finish off the OrderRepository and add the call to the Program.cs
 * file. Apparently it doens't need to be in a OrderService because it a aggregate.
 * trying to replace AutoMapper. Manual approach, Mapperly, or Mapster are the replacements for AutoMapper.
 * 
 *
 * 
 *
 *---------------------------
 * TODO NEXT WEEK/WHERE IM UP DO
 *---------------------------
 * Following this https://code-maze.com/csharp-design-pattern-aggregate/
 * Up to creating the OrderReposityr and adding it to the Program.cs file. bit unsure about the OrderDTO part and where
 * it fits in all of this. This also might be a good time to break off and focus on implementing our own AutoMapper or
 * potentially utalise one of the other approaches such as Mapperly or Mapster.
 * 
 * 
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
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
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

// API --> service --> unitOfWOrk --> repository --> db queries

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
app.MapPut("/order/new", async (int customerId, List<OrderLineDTO> orderLines, IUnitOfWork unitOfWork) =>
    {
        // Products exist
        // Ensure customer exists
        // Ensure stock available
        // Reserve stock
        
        // Call OrderRepository here, don't need OrderService as business logic is containe in the aggreggates. for non aggregate
        // the buisness logic is handled in the service.cs file
        await unitOfWork.Orders.CreateOrder();
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
