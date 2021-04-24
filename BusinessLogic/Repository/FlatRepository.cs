using AutoMapper;
using BusinessLogic.Repository.IRepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public class FlatRepository : IFlatRepository
    {
        private ApplicationDbContext _db;
        private IMapper _mapper;

        public FlatRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<FlatDTO> Create(FlatDTO modelDTO)
        {
            var model = _mapper.Map<FlatDTO, Flat>(modelDTO);

            model.CreatedDate = DateTime.Now;
            model.CreatedBy = "";

            var create = _db.Flats.Add(model);
            await _db.SaveChangesAsync();

            return _mapper.Map<Flat, FlatDTO>(create.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var model = await _db.Flats.FirstOrDefaultAsync(x => x.Id == id);

            if (model != null)
            {
                var allImages = await _db.FlatImages.Where(x => x.FlatId == id).ToListAsync();

                _db.FlatImages.RemoveRange(allImages);

                _db.Flats.Remove(model);

                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<FlatDTO> Get(int id, string checkInDate = null, string checkOutDate = null)
        {
            var model = _mapper.Map<Flat, FlatDTO>(await _db.Flats
                .Include(x => x.FlatImages)
                .FirstOrDefaultAsync(x => x.Id == id));

            if (!string.IsNullOrEmpty(checkInDate) && !string.IsNullOrEmpty(checkOutDate))
            {
                model.IsBooked = await IsBooked(id, checkInDate, checkOutDate);
            }

            return model;
        }

        public async Task<IEnumerable<FlatDTO>> GetAll(string checkInDate = null, string checkOutDate = null)
        {
            var modelsDTO = _mapper.Map<IEnumerable<Flat>, IEnumerable<FlatDTO>>(_db.Flats.Include(x => x.FlatImages));

            if (!string.IsNullOrEmpty(checkInDate) && !string.IsNullOrEmpty(checkOutDate))
            {
                foreach (var flat in modelsDTO)
                {
                    flat.IsBooked = await IsBooked(flat.Id, checkInDate, checkOutDate);
                }
            }

            return modelsDTO;
        }

        public async Task<FlatDTO> IsNameUnique(string name, int id = 0)
        {
            if(id == 0)
            {
                var modelDTO = _mapper.Map<Flat, FlatDTO>(await _db.Flats.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower()));

                return modelDTO;
            }
            else
            {
                var modelDTO = _mapper.Map<Flat, FlatDTO>(await _db.Flats.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower()
                && x.Id != id));

                return modelDTO;
            }
        }

        public async Task<FlatDTO> Update(int id, FlatDTO modelDTO)
        {
            if(id == modelDTO.Id)
            {
                var modelDetails = await _db.Flats.FirstOrDefaultAsync(x => x.Id == id); //Flat
                var model = _mapper.Map<FlatDTO, Flat>(modelDTO, modelDetails); //Flat

                model.UpdatedBy = "";
                model.UpdateDate = DateTime.Now;

                var updateModel = _db.Flats.Update(model);
                await _db.SaveChangesAsync();

                return _mapper.Map<Flat, FlatDTO>(updateModel.Entity);
            }
            else
            {
                return null;
            }
        }


        public async Task<bool> IsBooked(int id, string checkInDatestr, string checkOutDatestr)
        {
            if (!string.IsNullOrEmpty(checkOutDatestr) && !string.IsNullOrEmpty(checkInDatestr))
            {
                DateTime checkInDate = DateTime.ParseExact(checkInDatestr, "MM/dd/yyyy", null);
                DateTime checkOutDate = DateTime.ParseExact(checkOutDatestr, "MM/dd/yyyy", null);

                var existingBooking = await _db.OrderDetails.Where(x => x.FlatId == id
                && x.IsPaymentSuccessful
                // Check if checkin date that user wants does not fall in between any dates for room that is booked .
                // Проверка, не попадает ли дата регистрации, которую хочет пользователь, между любыми датами забронированного номера
                && ((checkInDate < x.CheckOutDate && checkInDate.Date >= x.CheckInDate)
                // Check if checkout date that user wants does not fall in between any dates for flat that is booked.
                // Проверка, не совпадает ли желаемая пользователем дата выезда с датами для забронированной квартиры.
                || (checkOutDate.Date > x.CheckInDate.Date && checkInDate.Date <= x.CheckInDate.Date)
                )).FirstOrDefaultAsync();

                if (existingBooking != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

    }
}
