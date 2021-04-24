using ClientApp.Services.IServices;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Models.DTOs;

namespace ClientApp.Services
{
    public class InfoBlockService : IInfoBlockService
    {
        // HttpClient отвечает за отправку HTTP-запросов и получения HTTP-ответов от ресурса, идентифицированного URI(унифицированный идентификатор ресурса).
        private readonly HttpClient _httpClient;

        public InfoBlockService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<InfoBlockDTO>> GetAllBlocks()
        {
            var response = await _httpClient.GetAsync($"api/infoblock");
            var content = await response.Content.ReadAsStringAsync();

            var objects = JsonConvert.DeserializeObject<IEnumerable<InfoBlockDTO>>(content);

            return objects;
        }
    }
}
