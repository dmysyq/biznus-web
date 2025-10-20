using System.ComponentModel.DataAnnotations;

namespace biznus_web.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string Email { get; set; } = string.Empty;

        [StringLength(5000, ErrorMessage = "Message cannot exceed 5000 characters")]
        public string Message { get; set; } = string.Empty;

        public bool IsSuccess { get; set; } = false;
        public string SuccessMessage { get; set; } = string.Empty;
    }
}
