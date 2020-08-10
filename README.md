# Payment Gateway API

Payment Gateway API built with ASP.NET Core 2.2 to process payments for merchants

## Frameworks and Libraries
- [ASP.NET Core 2.2](https://docs.microsoft.com/pt-br/aspnet/core/?view=aspnetcore-2.2)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/) (for data access)
- [AutoMapper](https://automapper.org/) (for mapping resources and models)
- [xUnit Test](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test)(Unit Test)
- [NLog](https://nlog-project.org/)(Application Logging)
- [Microsoft SQL Server](https://docs.microsoft.com/en-us/sql/sql-server/what-s-new-in-sql-server-2017?view=sql-server-ver15)(Data Storage)
- [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle) (API documentation)

## Functionalities
- Merchant can process payment (POST) through the api
- Merchant can get details of a specific payment (GET)

## How to Test

First, install [.NET Core 2.2](https://dotnet.microsoft.com/download/dotnet-core/2.2). Then, open the terminal or command prompt at the API root path (```/src/PaymentGatewayApi/```) 
and run the following command:

```
dotnet restore
dotnet run
```

Navigate to ```https://localhost:5001/api/payment/GetAllPayments```

Navigate to ```https://localhost:5001/swagger``` to check the API documentation.

To test endpoints, you'll need to use a software such as [Postman](https://www.getpostman.com/).
