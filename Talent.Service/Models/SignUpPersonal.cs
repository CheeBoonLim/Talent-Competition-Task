using System;

namespace Talent.Service.Models
{
    // Information about the person who is signing up 
    public class SignUpPersonal
    {
        public SignUpPersonal()
        {
            SendEmailVerificationEmail = true;
            SendWelcomeMail = true;
        }
        public int id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public string CountryCode { get; set; }
        public string DialCode { get; set; }
        public int GenderId { get; set; }
        public bool ExistingAccount { get; set; }
        public bool TermsConditionsAccepted { get; set; }
        public bool SendEmailVerificationEmail { get; set; }
        public bool SendWelcomeMail { get; set; }
        public Guid EmailCode { get; set; }
    }
}
