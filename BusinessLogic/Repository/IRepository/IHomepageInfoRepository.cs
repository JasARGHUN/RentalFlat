using Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Repository.IRepository
{
    public interface IHomepageInfoRepository
    {
        public Task<HomepageInfoDTO> Create(HomepageInfoDTO modelDTO);
        public Task<HomepageInfoDTO> Update(int id, HomepageInfoDTO modelDTO);
        public Task<HomepageInfoDTO> Get(int id);
        public Task<IEnumerable<HomepageInfoDTO>> GetAll();
        public Task<int> Delete(int id);
    }
}
