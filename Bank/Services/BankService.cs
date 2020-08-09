using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Bank.DTO;
using Bank.Enums;
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

            // get pending payments only from list of payments
            var pendingPayments = GetPendingPayments(payments, x => (PaymentStatus)x.Status == PaymentStatus.Pending);

            // return response to gateway api
            await PutPayment(pendingPayments);


            return payments;
        }

        public async Task<HttpStatusCode> PutPayment(List<PaymentDTO> entity)
        {
            var jsonString = JsonConvert.SerializeObject(entity);

            HttpContent httpContent = new StringContent(jsonString);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage Response = await _client.PutAsync("api/Payment/GetAllPayments", httpContent);

            if (Response.IsSuccessStatusCode)
            {
                Console.WriteLine("Payment Response successfull");
            }

            return Response.StatusCode;
        }

        private List<PaymentDTO> GetPendingPayments(List<PaymentDTO> payments, Func<PaymentDTO, bool> filter)
        {
            return payments.Where(filter).ToList();
        }
    }
}
