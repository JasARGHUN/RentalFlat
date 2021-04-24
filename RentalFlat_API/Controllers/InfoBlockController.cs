using BusinessLogic.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace RentalFlat_API.Controllers
{
    [Route("api/[controller]")]
    public class InfoBlockController : Controller
    {
        private readonly IInfoBlockRepository _repository;

        public InfoBlockController(IInfoBlockRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var objList = await _repository.GetAll();

            return Ok(objList);
        }
    }
}
