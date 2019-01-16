using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Talent.WebApp.Models.Profile
{
    public class LanguagePersonListViewModel
    {
        public int PersonLanguageId { get; set; }
        public string Language { get; set; }
        public string LanguageLevel { get; set; }
    }

    public class AddLanguageViewModel
    {
        public string Name { get; set; }
        public string Level { get; set; }
        public int Id { get; set; }
    }
}