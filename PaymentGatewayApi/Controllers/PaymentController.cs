using BusinessEntites.Entities;
using BusinessServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace PaymentGatewayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        // GET: api/Payment/GetPaymentDetailsById/1
        [HttpGet("GetPaymentDetailsById/{id}")]
        public async Task<IActionResult> GetPaymentDetailsById(int id)
        {
            var payment = await _paymentService.GetPaymentById(id);
            if (payment != null)
                return Ok(payment);
            return NotFound();
        }

        // POST: api/Payment/ProcessPayment
        [HttpPost("ProcessPayment")]
        public async Task<IActionResult> ProcessPayment([FromBody] Payment entity)
        {
            try {
                if (entity == null)
                {
                    _logger.LogInformation("Payment Entity is null");
                    return NotFound();
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogInformation("Invalid Payment object sent from client");
                    return BadRequest();
                }
                int paymentId = await _paymentService.ProcessPayment(entity);
                if (paymentId == 0)
                {
                    _logger.LogInformation("Failed to process payment");
                    return BadRequest();
                }
                return Ok();
            }
            catch(Exception exc)
            {
                _logger.LogInformation($"Something went wrong inside ProcessPayment action: {exc.Message}");
                return StatusCode(500, "Internal server error"); 
            }         
        }
    }
}
