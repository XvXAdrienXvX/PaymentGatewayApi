using BusinessEntites.Entities;
using BusinessServices.DTO;
using BusinessServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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

        // GET: api/Payment/GetAllPayments
        [HttpGet("GetAllPayments")]
        public async Task<ActionResult> GetAllPayments()
        {
            var payments = await _paymentService.GetAllPayments();
            if (payments != null)
                return Ok(payments);
            return NotFound();
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
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentDTO entity)
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

        [HttpPut("UpdatePaymentById")]
        public async Task<bool> Put(int id, [FromBody]PaymentDTO entity)
        {
            if (id > 0)
            {
                return await _paymentService.UpdatePaymentById(id, entity);
            }
            return false;
        }

        [HttpPut("UpdatePayment")]
        public async Task<bool> UpdatePayment([FromBody] List<PaymentDTO> entity)
        {
            if (entity.Any())
            {
                return await _paymentService.UpdatePayment(entity);
            }
            return false;
        }
    }
}
