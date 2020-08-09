using BusinessEntites.Entities;
using BusinessServices.DTO;
using BusinessServices.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGatewayTest.Fake
{
    public class PaymentServiceFake : IPaymentService
    {
        private readonly List<PaymentDTO> _payment;
        private const string fileName = "PaymentTestData.json";

        public PaymentServiceFake()
        {
           _payment = JsonConvert.DeserializeObject<List<PaymentDTO>>(File.ReadAllText(Path.GetFullPath(fileName)));
        }

        public async Task<List<PaymentDTO>> GetAllPayments()
        {
            return  _payment;
        }

        public Task<List<CardDetails>> GetCardDetails()
        {
            throw new NotImplementedException();
        }

        public async Task<PaymentDTO> GetPaymentById(int Id)
        {
            return _payment.Where(x => x.PaymentId == Id).FirstOrDefault();
        }

        public async Task<int> ProcessPayment(PaymentDTO entity)
        {
            entity.PaymentId = 2;
            _payment.Add(entity);
            return entity.PaymentId;
        }

        public Task<bool> UpdatePayment(List<PaymentDTO> entityList)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatePaymentById(int Id, PaymentDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
