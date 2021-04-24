using Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Repository.IRepository
{
    public interface IImageRepository
    {
        public Task<int> CreateImage(HomeImageDTO model);
        public Task<int> DeleteImageByImageId(int id);
        public Task<int> DeleteImageByInfoId(int id);
        public Task<int> DeleteImageByUrl(string imageUrl);
        public Task<IEnumerable<HomeImageDTO>> GetImage(int flatId);
    }
}
