using Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Repository.IRepository
{
    public interface IFlatImagesRepository
    {
        public Task<int> CreateFlatImage(FlatImageDTO model);
        public Task<int> DeleteFlatImageByImageId(int id);
        public Task<int> DeleteFlatImageByRoomId(int id);
        public Task<int> DeleteImageByUrl(string imageUrl);
        public Task<IEnumerable<FlatImageDTO>> GetFlatImages(int flatId);
    }
}
