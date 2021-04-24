using ClientApp.Services.IServices;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Models.DTOs;
using Models;

namespace ClientApp.Services
{
    public class FlatService : IFlatService
    {
        // HttpClient отвечает за отправку HTTP-запросов и получения HTTP-ответов от ресурса, идентифицированного URI(унифицированный идентификатор ресурса).
        private readonly HttpClient _httpClient;

        public FlatService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Calling the RentalFlat.API/Controllers/FlatController. Don't forget to change settings in wwwroot/appsettings.json for your API URL!!!
        public async Task<IEnumerable<FlatDTO>> GetAllFlats(string checkInDate, string checkOutDate)
        {
            var response = await _httpClient.GetAsync($"api/flat?checkInDate={checkInDate}&checkOutDate={checkOutDate}");
            var content = await response.Content.ReadAsStringAsync();

            var objects = JsonConvert.DeserializeObject<IEnumerable<FlatDTO>>(content);

            return objects;
        }

        public async Task<FlatDTO> GetFlat(int id, string checkInDate, string checkOutDate)
        {
            var response = await _httpClient.GetAsync($"api/flat/{id}?checkInDate={checkInDate}&checkOutDate={checkOutDate}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<FlatDTO>(content);

                return obj;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject<Error>(content);

                throw new Exception(error.ErrorMessage);
            }
        }
    }
}
