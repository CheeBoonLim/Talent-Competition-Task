using Talent.Data.Localization;
using Talent.WebApp.Content.Resources;
using System.ComponentModel.DataAnnotations;

namespace Talent.WebApp.Models.Account
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(150, MinimumLength = 6)]
        public string Password { get; set; }

        public string ReturnURL { get; set; }

        public bool IsRemember { get; set; }

        public string RecaptchaSiteKey { get; set; }
    }
}