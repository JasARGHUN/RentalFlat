using Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientApp.Services.IServices
{
    public interface IInfoBlockService
    {
        public Task<IEnumerable<InfoBlockDTO>> GetAllBlocks();
    }
}
