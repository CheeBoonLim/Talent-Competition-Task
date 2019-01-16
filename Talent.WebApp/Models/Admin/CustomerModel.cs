using Talent.Core;
using Talent.Data.Entities;
using Talent.Service.Domain;

namespace Talent.WebApp.Models.Admin
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string CreatedOn { get; set; }
        public string CreatedOnHowLongAgo { get; set; }
        public string TotalBought { get; set; }
        public string TotalSold { get; set; }

        public static CustomerModel Create(Person n, IApplicationContext appContext)
        {
            var model = new CustomerModel
            {
                Id = n.Id,
                Status = ((VerificationLevel)n.VerifiMarsionLevel).ToString(),
                Name = string.Format("{0} {1}", n.FirstName, n.LastName),
                Email = n.Login.Username,
                Mobile = n.MobilePhone,
                CreatedOn = n.CreatedDate.ToString(),
                CreatedOnHowLongAgo = n.CreatedDate.HowLongAgoRoughly()
            };

            return model;
        }
    }
}