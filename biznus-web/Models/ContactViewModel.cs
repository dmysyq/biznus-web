using System.ComponentModel.DataAnnotations;

namespace biznus_web.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public bool IsSuccess { get; set; } = false;
        public string SuccessMessage { get; set; } = string.Empty;
    }
}
