using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Talent.WebApp.Models.Profile
{
    public class SkillPersonListViewModel
    {
        public int PersonSkillId { get; set; }
        public string Skill { get; set; }
        public string ExperienceLevel { get; set; }
    }

    public class AddSkillViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Level { get; set; }
    }
}