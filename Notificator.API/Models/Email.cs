using System.ComponentModel.DataAnnotations;

namespace Notificator.API.Models
{
    public class Email
    {
        [Required(ErrorMessage = "Required email address")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Required subject")]
        [StringLength(40, ErrorMessage = "Subject length must be inferior than 40")]
        public string Subject { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "String length must be inferior than 100")]
        public string? Body { get; set; }
    }
}
