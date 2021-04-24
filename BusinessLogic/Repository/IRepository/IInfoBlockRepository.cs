using Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Repository.IRepository
{
    public interface IInfoBlockRepository
    {
        public Task<InfoBlockDTO> Create(InfoBlockDTO modelDTO);
        public Task<InfoBlockDTO> Update(int id, InfoBlockDTO modelDTO);
        public Task<InfoBlockDTO> Get(int id);
        public Task<IEnumerable<InfoBlockDTO>> GetAll();
        public Task<int> Delete(int id);
    }
}
