using Models.DTOs;
using Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ClientApp.Services.IServices;

namespace ClientApp.Services
{
    public class StripePaymentService : IStripePaymentService
    {
        // HttpClient отвечает за отправку HTTP-запросов и получения HTTP-ответов от ресурса, идентифицированного URI(унифицированный идентификатор ресурса).
        private readonly HttpClient _client;

        public StripePaymentService(HttpClient client)
        {
            _client = client;
        }

        public async Task<SuccessModel> CheckOut(StripePaymentDTO model)
        {
            var content = JsonConvert.SerializeObject(model);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/stripepayment/create", bodyContent);

            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<SuccessModel>(contentTemp);

                return result;
            }
            else
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject<Error>(contentTemp);

                throw new Exception(error.ErrorMessage);
            }
        }
    }
}
