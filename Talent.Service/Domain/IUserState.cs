using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talent.Service.Domain
{
    public interface IUserState
    {
        int Id { get; set; }

        string Language { get; set; }

        string Culture { get; set; }

        string TimezoneIdentifier { get; }
        string Role { get; }
    }

    public class UserState : IUserState
    {
        //public UserState(int id, Org org, string language, string culture, string timezoneIdentifier)
        //{
        //    Id = id;
        //    Org = org;
        //    Language = language;
        //    Culture = culture;
        //    TimezoneIdentifier = timezoneIdentifier;
        //}
        public int Id
        {
            get;
            set;
        }

        public string Language
        {
            get;
            set;
        }

        public string Culture
        {
            get;
            set;
        }

        public string TimezoneIdentifier
        {
            get;
            set;
        }

        public string Role
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }
    }
}
