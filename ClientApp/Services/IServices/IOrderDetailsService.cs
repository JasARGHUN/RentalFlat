using Models.DTOs;
using System.Threading.Tasks;

namespace ClientApp.Services.IServices
{
    public interface IOrderDetailsService
    {
        public Task<OrderDetailsDTO> SaveOrderDetails(OrderDetailsDTO details);
        public Task<OrderDetailsDTO> MarkPaymentSuccessful(OrderDetailsDTO details);
    }
}
