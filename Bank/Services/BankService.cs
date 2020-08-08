using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Bank.DTO;
using Newtonsoft.Json;

namespace Bank.Services
{
    public class BankService : IBankService
    {
        private readonly HttpClient _client;

        public BankService(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("http://localhost:50325");
            _client.DefaultRequestHeaders.Clear();
        }

        public async Task<List<PaymentDTO>> CapturePaymentAsync()
        {
            var payments = new List<PaymentDTO>();
           
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
       
            HttpResponseMessage Response =  _client.GetAsync("api/Payment/GetAllPayments").Result;
            if (Response.IsSuccessStatusCode)
            {
                var content = await Response.Content.ReadAsStringAsync();
                payments = JsonConvert.DeserializeObject<List<PaymentDTO>>(content);
            }
            else
            {
                Console.WriteLine("Internal server Error");
            }

            return payments;
        }
    }
}
