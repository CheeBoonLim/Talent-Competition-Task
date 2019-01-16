using System;
using System.ComponentModel.DataAnnotations;

namespace Talent.WebApp.Models.Account
{
    public class RegisterModel
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(150, MinimumLength = 8)]
        public string Password { get; set; }

        //[Required]
        //public string CountryDialCode { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        //public string PasswordSalt { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        //[Required]
        //public string MobilePhone { get; set; }
        //public int GenderId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public string RecaptchaSiteKey { get; set; }
    }

    public class EmailModel
    {
        [Required]
        public string Email { get; set; }
    }
}