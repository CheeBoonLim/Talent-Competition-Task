using Talent.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talent.Service.Domain
{
    public class Verification
    {
        public VerificationLevel Level { get; set; }
        public string LevelName { get; set; }
        public decimal DailyLimit { get; set; }
    }
}
