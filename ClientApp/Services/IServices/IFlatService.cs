using Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientApp.Services.IServices
{
    public interface IFlatService
    {
        //Get all flats.
        public Task<IEnumerable<FlatDTO>> GetAllFlats(string checkInDate, string checkOutDate);

        //Get individual flat.
        public Task<FlatDTO> GetFlat(int flatId, string checkInDate, string checkOutDate);
    }
}
