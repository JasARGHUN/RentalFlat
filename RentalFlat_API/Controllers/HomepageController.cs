using BusinessLogic.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Threading.Tasks;


namespace RentalFlat_API.Controllers
{
    [Route("api/[controller]")]
    public class HomepageController : Controller
    {
        private readonly IHomepageInfoRepository _homepageRepository;

        public HomepageController(IHomepageInfoRepository homepageRepository)
        {
            _homepageRepository = homepageRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var model = await _homepageRepository.GetAll();

            //if (model == null)
            //{
            //    return BadRequest(new Error()
            //    {
            //        StatusCode = StatusCodes.Status400BadRequest,
            //        ErrorMessage = "Empty!"
            //    });
            //}

            return Ok(model);
        }
    }
}
