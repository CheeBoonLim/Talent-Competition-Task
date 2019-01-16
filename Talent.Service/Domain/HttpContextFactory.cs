using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Talent.Service.Domain
{
    public class HttpContextFactory : IHttpContextFactory
    {
        public virtual HttpContextBase Create()
        {
            return new HttpContextWrapper(HttpContext.Current);
        }
    }
}
