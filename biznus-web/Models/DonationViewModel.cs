using System.ComponentModel.DataAnnotations;

namespace biznus_web.Models
{
    public class DonationViewModel
    {
        [Required(ErrorMessage = "Amount is required")]
        [Range(1, 10000, ErrorMessage = "Amount must be between $1 and $10,000")]
        public decimal Amount { get; set; }

        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string DonorName { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string DonorEmail { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Message cannot exceed 500 characters")]
        public string Message { get; set; } = string.Empty;

        public bool IsSuccess { get; set; } = false;
        public string SuccessMessage { get; set; } = string.Empty;
    }
}
