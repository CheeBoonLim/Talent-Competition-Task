using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Talent.Service.Domain
{
    public interface IHttpContextFactory
    {
        HttpContextBase Create();
    }
}
