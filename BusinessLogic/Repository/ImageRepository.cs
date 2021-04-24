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
    public class ImageRepository : IImageRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ImageRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<int> CreateImage(HomeImageDTO model)
        {
            var obj = _mapper.Map<HomeImageDTO, HomeImage>(model);
            await _db.HomeImages.AddAsync(obj);

            return await _db.SaveChangesAsync();
        }

        public async Task<int> DeleteImageByImageId(int id)
        {
            var model = await _db.HomeImages.FindAsync(id);

            _db.HomeImages.Remove(model);

            return await _db.SaveChangesAsync();
        }

        public async Task<int> DeleteImageByInfoId(int id)
        {
            var modelList = await _db.HomeImages.Where(x => x.HomepageInfoId == id).ToListAsync();

            _db.HomeImages.RemoveRange(modelList);

            return await _db.SaveChangesAsync();
        }

        public async Task<int> DeleteImageByUrl(string imageUrl)
        {
            var image = await _db.HomeImages.FirstOrDefaultAsync
                (x => x.ImageUrl.ToLower() == imageUrl.ToLower());

            if (image == null)
            {
                return 0;
            }

            _db.HomeImages.Remove(image);

            return await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<HomeImageDTO>> GetImage(int flatId)
        {
            return _mapper.Map<IEnumerable<HomeImage>, IEnumerable<HomeImageDTO>>
                (await _db.HomeImages.Where(x => x.HomepageInfoId == flatId).ToListAsync());
        }
    }
}
