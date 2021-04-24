using AutoMapper;
using BusinessLogic.Repository.IRepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public class InfoBlockRepository : IInfoBlockRepository
    {
        private ApplicationDbContext _db;
        private IMapper _mapper;

        public InfoBlockRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<InfoBlockDTO> Create(InfoBlockDTO modelDTO)
        {
            var model = _mapper.Map<InfoBlockDTO, InfoBlock>(modelDTO);

            var create = _db.InfoBlocks.Add(model);
            await _db.SaveChangesAsync();

            return _mapper.Map<InfoBlock, InfoBlockDTO>(create.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var obj = await _db.InfoBlocks.FirstOrDefaultAsync(x => x.Id == id);

            if(obj != null)
            {
                _db.InfoBlocks.Remove(obj);
                await _db.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<InfoBlockDTO> Get(int id)
        {
            var obj = _mapper.Map<InfoBlock, InfoBlockDTO>(await _db.InfoBlocks.FirstOrDefaultAsync(x => x.Id == id));

            return obj;
        }

        public async Task<IEnumerable<InfoBlockDTO>> GetAll()
        {
            var objList = _mapper.Map<IEnumerable<InfoBlock>, IEnumerable<InfoBlockDTO>>(_db.InfoBlocks);

            return objList;
        }

        public async Task<InfoBlockDTO> Update(int id, InfoBlockDTO modelDTO)
        {
            if(id == modelDTO.Id)
            {
                var obj = await _db.InfoBlocks.FirstOrDefaultAsync(x => x.Id == id);
                var model = _mapper.Map<InfoBlockDTO, InfoBlock>(modelDTO, obj);

                var updateObj = _db.InfoBlocks.Update(model);
                await _db.SaveChangesAsync();

                return _mapper.Map<InfoBlock, InfoBlockDTO>(updateObj.Entity);
            }
            else
            {
                return null;
            }
        }
    }
}
