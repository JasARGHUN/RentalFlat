using System;
using System.ComponentModel.DataAnnotations;

namespace Models.DTOs
{
    public class OrderDetailsDTO
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string StripeSessionId { get; set; }

        [Required]
        public DateTime CheckInDate { get; set; }

        [Required]
        public DateTime CheckOutDate { get; set; }

        public DateTime ActualCheckInDate { get; set; }
        public DateTime ActualCheckOutDate { get; set; }

        [Required]
        public double TotalCost { get; set; }

        [Required]
        public int FlatId { get; set; }

        public bool IsPaymentSuccessful { get; set; } = false;

        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }

        public FlatDTO FlatDTO { get; set; }

        public string Status { get; set; }
    }
}
