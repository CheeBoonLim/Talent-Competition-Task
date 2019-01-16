using Microsoft.AspNet.Identity;
using Talent.Data;
using Talent.Data.Entities;
using System;
using System.Linq;
using System.Web;
using Talent.Core;

namespace Talent.Service.Domain
{
    //public class ApplicationContext : IApplicationContext
    //{
    //    // Using a readonly variable ensures the value can only be set in the constructor
    //    private readonly IHttpContextFactory _httpContextFactory;

    //    private HttpContextBase _httpContext = null;
    //    private Guid _userId = Guid.Empty;
    //    private string _userName = null;
    //    private int? _loginId = null;
    //    private bool? _isAdmin = false;
    //    private string _timezone = null;
    //    private string _fullName = null;

    //    private IRepository<Login> _loginRepository;

    //    public ApplicationContext(IHttpContextFactory httpContextFactory,
    //                                IRepository<Login> loginRepository)
    //    {
    //        // Using a guard clause ensures that if the DI container fails
    //        // to provide the dependency you will get an exception
    //        if (httpContextFactory == null) throw new ArgumentNullException("httpContextFactory");

    //        _httpContextFactory = httpContextFactory;
    //        _loginRepository = loginRepository;
    //    }

    //    // Singleton pattern to access HTTP context at the right time
    //    private HttpContextBase HttpContext
    //    {
    //        get
    //        {
    //            if (this._httpContext == null)
    //            {
    //                this._httpContext = _httpContextFactory.Create();
    //            }
    //            return this._httpContext;
    //        }
    //    }

    //    /// <summary>
    //    /// Get login Id
    //    /// </summary>
    //    public int LoginId
    //    {
    //        get
    //        {
    //            var user = this.HttpContext.User;
    //            if (this._loginId == null && user != null && user.Identity.IsAuthenticated)
    //            {
    //                var id = _loginRepository.GetQueryable().Where(x => x.Username.Equals(user.Identity.Name)).Select(x => x.Id).FirstOrDefault();
    //                this._loginId = id;
    //            }
    //            return this._loginId.GetValueOrDefault();
    //        }
    //        set { this._loginId = value; }
    //    }

    //    /// <summary>
    //    /// Get login username
    //    /// </summary>
    //    public string UserName
    //    {
    //        get
    //        {
    //            var user = this.HttpContext.User;

    //            if (this._userName == null && user != null && user.Identity.IsAuthenticated)
    //            {
    //                this._userName = user.Identity.Name;
    //            }

    //            return this._userName;
    //        }
    //        set { this._userName = value; }
    //    }

    //    public bool IsAdmin
    //    {
    //        get
    //        {
    //            var user = this.HttpContext.User;

    //            if (this._isAdmin == null)
    //            {
    //                if (user != null && user.Identity.IsAuthenticated)
    //                {
    //                    var person = _loginRepository.GetQueryable().FirstOrDefault(x => x.Username.Equals(user.Identity.Name));
    //                    this._isAdmin = person.IsAdmin;
    //                }

    //                this._isAdmin = false;
    //            }

    //            return this._isAdmin.Value;
    //        }
    //        set { this._isAdmin = value; }
    //    }

    //    public string TimeZone
    //    {
    //        get
    //        {
    //            var user = this.HttpContext.User;

    //            if (this._timezone == null)
    //            {
    //                if (user != null && user.Identity.IsAuthenticated)
    //                {
    //                    var person = _loginRepository.GetQueryable().FirstOrDefault(x => x.Username.Equals(user.Identity.Name));
    //                    //this._timezone = person.Timezone;
    //                }

    //                this._timezone = "New Zealand Standard Time";
    //            }

    //            return this._timezone;
    //        }
    //        set { this._timezone = value; }
    //    }

    //    public string FullName
    //    {
    //        get
    //        {
    //            var user = this.HttpContext.User;

    //            if (this._fullName == null)
    //            {
    //                if (user != null && user.Identity.IsAuthenticated)
    //                {
    //                    var login = _loginRepository.GetQueryable().FirstOrDefault(x => x.Username.Equals(user.Identity.Name));
    //                    if(login != null && login.Person != null)
    //                    {
    //                        this._fullName = string.Format("{0} {1}", login.Person.FirstName, login.Person.LastName);
    //                    }
    //                }
    //            }

    //            return this._fullName;
    //        }
    //        set { this._fullName = value; }
    //    }
    //}
}
