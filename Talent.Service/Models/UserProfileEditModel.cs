using System;
using System.ComponentModel.DataAnnotations;

namespace Talent.Service.Models
{
    public class UserProfileEditModel
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string CountryDialCode { get; set; }
        [Required]
        public string Mobile { get; set; }
        public DateTime? DOB { get; set; }

        public string FlatNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
        public string AdminNote { get; set; }

    }
}