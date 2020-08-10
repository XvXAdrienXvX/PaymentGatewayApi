using BusinessServices.DTO;
using BusinessServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentGatewayApi.Controllers;
using PaymentGatewayTest.Fake;
using System;
using Xunit;

namespace PaymentGatewayTest
{
    public class PaymentControllerTest
    {
        private PaymentController _paymentController;
        private IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;

        public PaymentControllerTest()
        {
            _paymentService = new PaymentServiceFake();
            _paymentController = new PaymentController(_paymentService, _logger);
        }

        [Fact]
        public void GetAllPayments()
        {
            var result = _paymentController.GetAllPayments();
            // verify GET returns Ok Status
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetPaymentById()
        {
            int Id = 1;
            var result = _paymentController.GetPaymentDetailsById(Id);

            // verify GET returns Ok Status
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetPaymentById_CorrectPaymentProcessed()
        {
            int Id = 1;
            var result = _paymentController.GetPaymentDetailsById(Id).Result as OkObjectResult;

            Assert.IsType<PaymentDTO>(result.Value);
            // verify if correct payment is returned
            Assert.Equal(Id, (result.Value as PaymentDTO).PaymentId);
        }

        [Fact]
        public void Post_ProcessPayment_ReturnsOkResponse()
        {
            PaymentDTO testData = new PaymentDTO()
            {
                 PaymentId =  2,
                 MerchantId = 1,
                 CardDetailsId = 1,
                 Amount = 1000,
                 Status = 1,
                 ProcessedDate = Convert.ToDateTime("2020-08-07"),
                 CardDetails = new CardDetailsDTO(){
                    CardType = new CardTypeDTO()
                    {
                       CardTypeId = 1,
                       Name = "VISA"
                    },
                    CardNumber = "AB24CD7114223158",
                    Cvv = 123,
                    ExpiryDate = Convert.ToDateTime("2025 - 03-29")
                 }
            };

            var createdResponse = _paymentController.ProcessPayment(testData);

            Assert.IsType<OkResult>(createdResponse.Result);
        }
    }
}
