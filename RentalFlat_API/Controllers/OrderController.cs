using BusinessLogic.Repository.IRepository;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTOs;
using Stripe.Checkout;
using System.Threading.Tasks;

namespace RentalFlat_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailSender _emailSender;

        public OrderController(IOrderRepository orderRepository, IEmailSender emailSender)
        {
            _orderRepository = orderRepository;
            _emailSender = emailSender;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderDetailsDTO detailsDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await _orderRepository.Create(detailsDTO);
                return Ok(result);
            }
            else
            {
                return BadRequest(new Error()
                {
                    ErrorMessage = "Error while creating flat details/ Booking"
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> PaymentSuccessful([FromBody] OrderDetailsDTO details)
        {
            var service = new SessionService();
            var sessionDetails = service.Get(details.StripeSessionId);

            if (sessionDetails.PaymentStatus == "paid")
            {
                var result = await _orderRepository.MarkPaymentSuccessful(details.Id);

                if (result == null)
                {
                    return BadRequest(new Error()
                    {
                        ErrorMessage = "Can't mark payment as successful"
                    });
                }

                await _emailSender.SendEmailAsync(details.Email, "Booking Confirmed!",
                    "Congratulation! Your booking has been confirmed! Your order ID: " + details.Id);

                return Ok(result);
            }
            else
            {
                return BadRequest(new Error()
                {
                    ErrorMessage = "Can't mark payment as successful"
                });
            }
        }
    }
}
