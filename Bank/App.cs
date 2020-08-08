using Bank.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public class App
    {
        private readonly IBankService _bankService;

        public App(IBankService bankService)
        {
            _bankService = bankService;
        }

        public async Task Run()
        {
            await _bankService.CapturePaymentAsync();
        }
    }
}
