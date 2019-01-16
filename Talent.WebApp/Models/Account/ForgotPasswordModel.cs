using Talent.Data.Localization;
using Talent.WebApp.Content.Resources;
using System.ComponentModel.DataAnnotations;

namespace Talent.WebApp.Models.Account
{
    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        [StringLength(150)]
        [LocalizedDisplayName(typeof(ViewModelLabels), "YourEmailAddress")]
        public string Email { get; set; }
        
        public string RecaptchaSiteKey { get; set; }
    }
}