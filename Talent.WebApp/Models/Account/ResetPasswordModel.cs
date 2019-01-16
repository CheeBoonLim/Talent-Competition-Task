using Talent.Data.Localization;
using Talent.WebApp.Content.Resources;
using System.ComponentModel.DataAnnotations;

namespace Talent.WebApp.Models.Account
{
    public class ResetPasswordModel
    {
        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; }

        [Required]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(150, MinimumLength = 8)]
        public string NewPassword { get; set; }

        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }

        public string RecaptchaSiteKey { get; set; }
    }
}