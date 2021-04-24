using Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Repository.IRepository
{
    public interface IFlatRepository
    {
        public Task<FlatDTO> Create(FlatDTO modelDTO);
        public Task<FlatDTO> Update(int id, FlatDTO modelDTO);
        public Task<FlatDTO> Get(int id, string checkInDate = null, string checkOutDate = null);
        public Task<IEnumerable<FlatDTO>> GetAll(string checkInDate = null, string checkOutDate = null);
        public Task<FlatDTO> IsNameUnique(string name, int id = 0);
        public Task<int> Delete(int id);
    }
}
