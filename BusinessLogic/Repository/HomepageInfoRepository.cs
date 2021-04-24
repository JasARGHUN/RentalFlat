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
    public class HomepageInfoRepository : IHomepageInfoRepository
    {
        private ApplicationDbContext _db;
        private IMapper _mapper;

        public HomepageInfoRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<HomepageInfoDTO> Create(HomepageInfoDTO modelDTO)
        {
            var model = _mapper.Map<HomepageInfoDTO, HomepageInfo>(modelDTO);

            var create = _db.HomepageInfos.Add(model);
            await _db.SaveChangesAsync();

            return _mapper.Map<HomepageInfo, HomepageInfoDTO>(create.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var model = await _db.HomepageInfos.FirstOrDefaultAsync(x => x.Id == id);

            if (model != null)
            {
                var allImages = await _db.HomeImages.Where(x => x.HomepageInfoId == id).ToListAsync();

                _db.HomeImages.RemoveRange(allImages);

                _db.HomepageInfos.Remove(model);

                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<HomepageInfoDTO> Get(int id)
        {
            var model = _mapper.Map<HomepageInfo, HomepageInfoDTO>(await _db.HomepageInfos
               .Include(x => x.Images)
               .FirstOrDefaultAsync(x => x.Id == id));

            return model;
        }

        public async Task<IEnumerable<HomepageInfoDTO>> GetAll()
        {
            var modelsDTO = _mapper.Map<IEnumerable<HomepageInfo>, IEnumerable<HomepageInfoDTO>>(_db.HomepageInfos.Include(x => x.Images));

            return modelsDTO;
        }

        public async Task<HomepageInfoDTO> Update(int id, HomepageInfoDTO modelDTO)
        {
            if (id == modelDTO.Id)
            {
                var modelDetails = await _db.HomepageInfos.FirstOrDefaultAsync(x => x.Id == id);
                var model = _mapper.Map<HomepageInfoDTO, HomepageInfo>(modelDTO, modelDetails);

                var updateModel = _db.HomepageInfos.Update(model);
                await _db.SaveChangesAsync();

                return _mapper.Map<HomepageInfo, HomepageInfoDTO>(updateModel.Entity);
            }
            else
            {
                return null;
            }
        }
    }
}
