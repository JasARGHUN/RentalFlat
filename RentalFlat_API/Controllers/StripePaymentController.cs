using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models.DTOs;
using Models;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalFlat_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StripePaymentController : Controller
    {
        private readonly IConfiguration _configuration;

        public StripePaymentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Create(StripePaymentDTO paymentDTO)
        {
            try
            {
                var domain = _configuration.GetValue<string>("Client.Url"); // Getting conf from appsettings.json/"Client.Url"

                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string>
                    {
                        "card"
                    },
                    LineItems = new List<SessionLineItemOptions>
                    {
                        new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                UnitAmount = paymentDTO.Amount*100, //convert to cents
                                Currency = "usd",
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = paymentDTO.ProductName
                                }
                            },
                            Quantity = 1
                        }
                    },

                    Mode = "payment",
                    SuccessUrl = domain + "/success-payment?session_id={{CHECKOUT_SESSION_ID}}",
                    CancelUrl = domain + paymentDTO.ReturnUrl
                };

                var service = new SessionService();

                Session session = await service.CreateAsync(options);

                return Ok(new SuccessModel()
                {
                    Data = session.Id
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Error()
                {
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}
