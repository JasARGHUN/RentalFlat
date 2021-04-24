using System;
using System.ComponentModel.DataAnnotations;

namespace ClientApp.Model.ViewModels
{
    public class HomeViewModel
    {
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }

        [Range(1, 100000, ErrorMessage = "Regular rate must be between 1")]
        public int NumberOfNights { get; set; } = 1;
    }
}
