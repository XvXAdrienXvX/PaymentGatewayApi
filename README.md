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
- Bank client & ValidatePayment to simulate/mock bank processing flow

## Database Schema
<img src="Screenshots/Screenshot(7).png" width="600">

## Assumptions
- Payments have a status
  ```
  Pending: 1,
  Approved: 2
  ```
  
- The acquiring bank is a client connecting to the gateway endpoint. To simulate/mock the bank, a console app is created in folder Bank which uses
  HttpClient to Get All Payments & check for pending payments. 
  
- ValidatePayment class & CardDetailsValidator class simulate the bank processing flow. The status of payments is updated from Pending to Approve

- A succesfull payment has an approved status

## How to Test
First, install [.NET Core 2.2](https://dotnet.microsoft.com/download/dotnet-core/2.2). Then, open the terminal or command prompt at the API root path (```/src/PaymentGatewayApi/```) 
and run the following command:

```
dotnet restore
dotnet run
```

Navigate to ```https://localhost:5001/api/payment/GetAllPayments```

Navigate to ```https://localhost:5001/index.html``` to check the API documentation.

To test endpoints, you'll need to use a software such as [Postman](https://www.getpostman.com/).
