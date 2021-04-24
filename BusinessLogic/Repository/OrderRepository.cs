using BusinessLogic.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using Models;
using Common;

namespace BusinessLogic.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrderRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrderDetailsDTO> Create(OrderDetailsDTO details)
        {
            details.CheckInDate = details.CheckInDate.Date;
            details.CheckOutDate = details.CheckOutDate.Date;

            var order = _mapper.Map<OrderDetailsDTO, OrderDetails>(details);

            order.Status = StaticDetails.Status_Pending;

            var result = await _context.OrderDetails.AddAsync(order);

            await _context.SaveChangesAsync();

            return _mapper.Map<OrderDetails, OrderDetailsDTO>(result.Entity);
        }

        public async Task<IEnumerable<OrderDetailsDTO>> GetAllOrderDetails()
        {
            IEnumerable<OrderDetailsDTO> orderDetails = _mapper.Map<IEnumerable<OrderDetails>, IEnumerable<OrderDetailsDTO>>
                (_context.OrderDetails.Include(x => x.Flat)).OrderByDescending(x => x.Id);

            return orderDetails;
        }

        public async Task<OrderDetailsDTO> GetOrderDetail(int orderId)
        {
            OrderDetails order = await _context.OrderDetails
                    .Include(u => u.Flat).ThenInclude(x => x.FlatImages)
                    .FirstOrDefaultAsync(u => u.Id == orderId);

            OrderDetailsDTO roomOrderDetailsDTO = _mapper.Map<OrderDetails, OrderDetailsDTO>(order);

            roomOrderDetailsDTO.FlatDTO.TotalDays = roomOrderDetailsDTO.CheckOutDate
                .Subtract(roomOrderDetailsDTO.CheckInDate).Days;

            return roomOrderDetailsDTO;
        }

        public async Task<bool> IsFlatBooked(int FlatId, DateTime checkInDate, DateTime checkOutDate)
        {
            var status = false;
            var existingBooking = await _context.OrderDetails.Where(x => x.FlatId == FlatId
                && x.IsPaymentSuccessful
                // Check if checkin date that user wants does not fall in between any dates for flat that is booked
                && (checkInDate < x.CheckOutDate && checkInDate.Date > x.CheckInDate
                    // Check if checkout date that user wants does not fall in between any dates for flat that is booked
                    || checkOutDate.Date > x.CheckInDate.Date && checkInDate.Date < x.CheckInDate.Date)
                ).FirstOrDefaultAsync();

            if (existingBooking != null)
            {
                status = true;
            }

            return status;
        }

        public async Task<OrderDetailsDTO> MarkPaymentSuccessful(int id)
        {
            var data = await _context.OrderDetails.FindAsync(id);

            if (data == null)
            {
                return null;
            }

            if (!data.IsPaymentSuccessful)
            {
                data.IsPaymentSuccessful = true;
                data.Status = StaticDetails.Status_Booked;

                var merkPaymentSuccessful = _context.OrderDetails.Update(data);
                await _context.SaveChangesAsync();

                return _mapper.Map<OrderDetails, OrderDetailsDTO>(merkPaymentSuccessful.Entity);
            }

            return new OrderDetailsDTO();
        }

        public async Task<bool> UpdateOrderStatus(int orderId, string status)
        {
            var order = await _context.OrderDetails.FirstOrDefaultAsync(u => u.Id == orderId);

            if (order == null)
            {
                return false;
            }

            order.Status = status;

            if (status == StaticDetails.Status_CheckedIn)
            {
                order.ActualCheckInDate = DateTime.Now;
            }

            if (status == StaticDetails.Status_CheckedOut_Completed)
            {
                order.ActualCheckOutDate = DateTime.Now;
            }

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
