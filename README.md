# Terry Training
## Order API

- Simple functionality – place an order with customer details and for order to be
fulfilled and stock to be received.
- Product
- Customer
- Order
- OrderLine

## Domain Model
[![](https://mermaid.ink/img/pako:eNp1U8tuwyAQ_BXEtcmhPVpVpSi5VIraSOmp8mUDxEHBrAtLpCjKvxcHGzsvXwwzu8wwwIkLlIoXXBjwfqGhclCXlsVv5VAGQWw6Zd9OKrfUViUmTyOHaTYi2oZ58IR1HBesMSCUT3yGY8lMSqe8jxVGH5RnQKnmWrxgL9oS0_IOXpPTtmIWavWMk8oLpxvSaB-uGs2I_RyDpef07ADawMZEkVR0CWqI4JRQllqatMKnzGhnpSO-stuu4S-AJU3HDG4NArFgNa2cFuoGN1HxBwlMws93lgY7OWvRDRaK4lZ8LlhqT-95Hx-9RX8jSYNcxDaIhgmsG6NIyQcuetnrXPQQiE-BVPHQ7VUcHeGDs2O4vyeQ_llyLNrXZNFhuTax18fw2z0sRkcxoA16ap_Jg_r28riu5cwnPO68Bi3jg7p4KTntVNwML-JQgtuXvLRtHQTC9dEKXpALasJDI4FU9_54sQXjI-owVLs8a8D-Ivbs-R8nBzHW?type=png)](https://mermaid.live/edit#pako:eNp1U8tuwyAQ_BXEtcmhPVpVpSi5VIraSOmp8mUDxEHBrAtLpCjKvxcHGzsvXwwzu8wwwIkLlIoXXBjwfqGhclCXlsVv5VAGQWw6Zd9OKrfUViUmTyOHaTYi2oZ58IR1HBesMSCUT3yGY8lMSqe8jxVGH5RnQKnmWrxgL9oS0_IOXpPTtmIWavWMk8oLpxvSaB-uGs2I_RyDpef07ADawMZEkVR0CWqI4JRQllqatMKnzGhnpSO-stuu4S-AJU3HDG4NArFgNa2cFuoGN1HxBwlMws93lgY7OWvRDRaK4lZ8LlhqT-95Hx-9RX8jSYNcxDaIhgmsG6NIyQcuetnrXPQQiE-BVPHQ7VUcHeGDs2O4vyeQ_llyLNrXZNFhuTax18fw2z0sRkcxoA16ap_Jg_r28riu5cwnPO68Bi3jg7p4KTntVNwML-JQgtuXvLRtHQTC9dEKXpALasJDI4FU9_54sQXjI-owVLs8a8D-Ivbs-R8nBzHW)

## API functions
- NewProduct(name, description, stockcount)
  - Check doesn’t already exist
  - Check name, description sizes
  - Check stockcount > 0
- AddStock(productid, amountAdded)
  - Check exists
  - Check amountAdded > 0
- CreateCustomer(givenName, surname, address details)
- GetCustomer(id)
- CreateOrder(customerId, orderLines)
    - Products exist
  - Ensure customer exists
  - Ensure stock available
  - Reserve stock
- FullfillOrder(orderId)
    - Ensure order exists
  - Update stock levels and reserve levels
  - Set order completed
- GetOrder(orderId)
  - Ensure order exists
  - Return all details except customer details

## Concepts to cover
- Domain (entities, aggregates, value objects)
- Persistence (code-first, migrations, repository, unit of work)
- Application (Command/Query segregation, Auto mapping, DTOs, fluent
validation, Result pattern)
- API (exception middleware)

## APPROACH
1. Create Persistence project (C# Class library)<br/>
    a. Create folder Models and create the four database models from diagram<br/>
    b. Create DBContext<br/>
2. Create Domain project (C# Class library)<br/>
    a. Create folder Entities, then inside that OrderAggregate<br/>
    b. In OrderAggregate, create Order and OrderLine classes<br/>
    c. Create Product and Customer class (inside Entities folder)<br/>
    d. Create ValueObjects folder and create Address class.<br/>
    e. Create folder Interfaces, create folder Repository<br/>
    f. Create repository interfaces<br/>
3. Create Application project (C# Class library)<br/>
    a. Create folder Interfaces, create IUnitOfWork interface<br/>
4. Create UnitOfWork and Repositories in Persistence project<br/>
    a. Use Automapper where possible<br/>
    b. Use DbContextFactory and generics for accessing repositories<br/>
5. In Application project,<br/>
    a. create Commands and Queries folders<br/>
    b. Create command and query classes where applicable<br/>
    c. Create DTO folder<br/>
    d. Create DTO for return data where applicable (get order, get customer)<br/>
    e. Create Result class<br/>
    f. Create Interfaces folder<br/>
    g. Create service interfaces<br/>
    h. Implement services<br/>
    - i. Use Fluent validation<br/>
        ii. Use UnitOfWork when using repositories for updates<br/>
        iii. Set and return Result object<br/>
        iv. Use Mapping to go from Domain to DTO’s<br/>
6. Create API project<br/>
    a. Create Controller folder<br/>
       i. Create controllers for endpoints<br/>
           1. Use services<br/>
       ii. Setup Program.cs<br/>
          1. Setup database<br/>
          2. Automatic migration<br/>
          3. Dependency Injection<br/>
          4. AutoMapper<br/>
  b. Create Exception handling middleware<br/>
