using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Repository.IRepository
{
    public interface IOrderRepository
    {
        public Task<OrderDetailsDTO> Create(OrderDetailsDTO details);
        public Task<IEnumerable<OrderDetailsDTO>> GetAllOrderDetails();
        public Task<OrderDetailsDTO> MarkPaymentSuccessful(int id);
        public Task<OrderDetailsDTO> GetOrderDetail(int orderId);
        public Task<bool> UpdateOrderStatus(int orderId, string status);
        public Task<bool> IsFlatBooked(int FlatId, DateTime checkInDate, DateTime checkOutDate);
    }
}
