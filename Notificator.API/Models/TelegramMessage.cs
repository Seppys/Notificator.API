using System.ComponentModel.DataAnnotations;

namespace Notificator.API.Models
{
    public class TelegramMessage
    {
        [Required(ErrorMessage = "Required username")]
        [StringLength(32, MinimumLength = 5, ErrorMessage = "Username length must be between 5 and 32")]
        [RegularExpression("^[a-zA-Z0-9_]*$", ErrorMessage = "Username must be conformed by a-z 0-9 and _")]
        public string username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Required message text")]
        [StringLength(100, ErrorMessage = "Text length must be inferior than 100")]
        public string text { get; set; } = string.Empty;
    }
}
