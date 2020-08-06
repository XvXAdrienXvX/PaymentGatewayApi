﻿using BusinessEntites.Entities;
using BusinessServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PaymentGatewayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public int Post([FromBody] Payment entity)
        {
            return _customerService.CreatePayment(entity);
        }
    }
}