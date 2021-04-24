using BusinessLogic.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using System.Globalization;
using Models;

namespace RentalFlat_API.Controllers
{
    [Route("api/[controller]")]
    public class FlatController : Controller
    {
        private readonly IFlatRepository _flatRepository;

        public FlatController(IFlatRepository flatRepository)
        {
            _flatRepository = flatRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetFlats(string checkInDate = null, string checkOutDate = null)
        {
            if (string.IsNullOrEmpty(checkInDate) || string.IsNullOrEmpty(checkOutDate))
            {
                return BadRequest(new Error()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "All parameters need to supplied!"
                });
            }

            // Enable these checks if you are sure that your time is set correctly. Otherwise, you'll have errors.
            //if (!DateTime.TryParseExact(checkInDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dataCheckingDate))
            //{
            //    return BadRequest(new Error()
            //    {
            //        StatusCode = StatusCodes.Status400BadRequest,
            //        ErrorMessage = "Invalid CheckIn date format. Valid format will be MM/dd/yyyy"
            //    });
            //}

            //if (!DateTime.TryParseExact(checkOutDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var datacheckOutDate))
            //{
            //    return BadRequest(new Error()
            //    {
            //        StatusCode = StatusCodes.Status400BadRequest,
            //        ErrorMessage = "Invalid CheckOut date format. Valid format will be MM/dd/yyyy"
            //    });
            //}

            var models = await _flatRepository.GetAll(checkInDate, checkOutDate);

            return Ok(models);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFlatById(int? id, string checkInDate = null, string checkOutDate = null)
        {
            if (id == null)
            {
                return BadRequest(new Error
                {
                    Title = "",
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid Id"
                });
            }

            if (string.IsNullOrEmpty(checkInDate) || string.IsNullOrEmpty(checkOutDate))
            {
                return BadRequest(new Error()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "All parameters need to supplied!"
                });
            }

            if (!DateTime.TryParseExact(checkInDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dataCheckingDate))
            {
                return BadRequest(new Error()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid CheckIn date format. Valid format will be MM/dd/yyyy"
                });
            }

            if (!DateTime.TryParseExact(checkOutDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var datacheckOutDate))
            {
                return BadRequest(new Error()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid CheckOut date format. Valid format will be MM/dd/yyyy"
                });
            }

            var model = await _flatRepository.Get(id.Value, checkInDate, checkOutDate);

            if (model == null)
            {
                return BadRequest(new Error
                {
                    Title = "",
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = "Invalid Id"
                });
            }

            return Ok(model);
        }
    }
}
