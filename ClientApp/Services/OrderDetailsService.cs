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
    public class OrderDetailsService : IOrderDetailsService
    {
        // HttpClient отвечает за отправку HTTP-запросов и получения HTTP-ответов от ресурса, идентифицированного URI(унифицированный идентификатор ресурса).
        private readonly HttpClient _httpClient;

        public OrderDetailsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<OrderDetailsDTO> MarkPaymentSuccessful(OrderDetailsDTO details)
        {
            var content = JsonConvert.SerializeObject(details);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/order/paymentsuccessful", bodyContent);

            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OrderDetailsDTO>(contentTemp);

                return result;
            }
            else
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject<Error>(contentTemp);

                throw new Exception(error.ErrorMessage);
            }
        }

        public async Task<OrderDetailsDTO> SaveOrderDetails(OrderDetailsDTO details)
        {
            var content = JsonConvert.SerializeObject(details);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/order/create", bodyContent);
            
            //string res = response.Content.ReadAsStringAsync().Result; // Need for debugging

            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OrderDetailsDTO>(contentTemp);

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
