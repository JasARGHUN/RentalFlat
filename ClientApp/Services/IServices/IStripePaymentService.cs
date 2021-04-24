using Models.DTOs;
using Models;
using System.Threading.Tasks;

namespace ClientApp.Services.IServices
{
    public interface IStripePaymentService
    {
        public Task<SuccessModel> CheckOut(StripePaymentDTO model);
    }
}
