# Terry Training
## Order API

- Simple functionality – place an order with customer details and for order to be
fulfilled and stock to be received.
- Product
- Customer
- Order
- OrderLine

- API functions
o NewProduct(name, description, stockcount)
§ Check doesn’t already exist
§ Check name, description sizes
§ Check stockcount > 0
o AddStock(productid, amountAdded)
§ Check exists
§ Check amountAdded > 0
o CreateCustomer(givenName, surname, address details)
o GetCustomer(id)
o CreateOrder(customerId, orderLines)
§ Products exist
§ Ensure customer exists
§ Ensure stock available
§ Reserve stock
o FullfillOrder(orderId)
§ Ensure order exists
§ Update stock levels and reserve levels
§ Set order completed
o GetOrder(orderId)
§ Ensure order exists
§ Return all details except customer details
- Concepts to cover
o Domain (entities, aggregates, value objects)
o Persistence (code-first, migrations, repository, unit of work)
o Application (Command/Query segregation, Auto mapping, DTOs, fluent
validation, Result pattern)
o API (exception middleware)
- APPROACH
1. Create Persistence project (C# Class library)
a. Create folder Models and create the four database models from diagram
b. Create DBContext
2. Create Domain project (C# Class library)
a. Create folder Entities, then inside that OrderAggregate
b. In OrderAggregate, create Order and OrderLine classes
c. Create Product and Customer class (inside Entities folder)
d. Create ValueObjects folder and create Address class.
e. Create folder Interfaces, create folder Repository
f. Create repository interfaces
3. Create Application project (C# Class library)
a. Create folder Interfaces, create IUnitOfWork interface
4. Create UnitOfWork and Repositories in Persistence project
a. Use Automapper where possible
b. Use DbContextFactory and generics for accessing repositories
5. In Application project,
a. create Commands and Queries folders
b. Create command and query classes where applicable
c. Create DTO folder
d. Create DTO for return data where applicable (get order, get customer)
e. Create Result class
f. Create Interfaces folder
g. Create service interfaces
h. Implement services
i. Use Fluent validation
ii. Use UnitOfWork when using repositories for updates
iii. Set and return Result object
iv. Use Mapping to go from Domain to DTO’s
6. Create API project
a. Create Controller folder
i. Create controllers for endpoints
1. Use services
ii. Setup Program.cs
1. Setup database
2. Automatic migration
3. Dependency Injection
4. AutoMapper
b. Create Exception handling middleware
