using AutoMapper;
using BusinessLogic.Repository.IRepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public class FlatImagesRepository : IFlatImagesRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public FlatImagesRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<int> CreateFlatImage(FlatImageDTO model)
        {
            var obj = _mapper.Map<FlatImageDTO, FlatImage>(model);
            await _db.FlatImages.AddAsync(obj);

            return await _db.SaveChangesAsync();
        }

        public async Task<int> DeleteFlatImageByImageId(int id)
        {
            var model = await _db.FlatImages.FindAsync(id);

            _db.FlatImages.Remove(model);

            return await _db.SaveChangesAsync();
        }

        public async Task<int> DeleteFlatImageByRoomId(int id)
        {
            var modelList = await _db.FlatImages.Where(x => x.FlatId == id).ToListAsync();

            _db.FlatImages.RemoveRange(modelList);

            return await _db.SaveChangesAsync();
        }

        public async Task<int> DeleteImageByUrl(string imageUrl)
        {
            var allImages = await _db.FlatImages.FirstOrDefaultAsync(x => x.FlatImageUrl.ToLower() == imageUrl.ToLower());

            if (allImages == null)
            {
                return 0;
            }

            _db.FlatImages.Remove(allImages);

            return await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<FlatImageDTO>> GetFlatImages(int flatId)
        {
            return _mapper.Map<IEnumerable<FlatImage>, IEnumerable<FlatImageDTO>>
                (await _db.FlatImages.Where(x => x.FlatId == flatId).ToListAsync());
        }
    }
}
