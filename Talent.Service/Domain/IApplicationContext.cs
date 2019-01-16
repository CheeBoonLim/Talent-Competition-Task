using Talent.Core;
using Talent.Data;
using Talent.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace Talent.Service.Domain
{
    public interface IApplicationContext
    {
        bool IsAuthenticated { get; }

        void SignIn(string username);

        void ChangeLanguage(string language);

        void ChangeCulture(string culture);

        IUserState CurrentUser { get; }
        void SessionAbandon();
        int LoginId { get; }
        string FullName { get; }
        string UserName { get; }
    }

    public class ApplicationContext : IApplicationContext
    {
        #region Data Members
        private readonly IHttpContextFactory _httpContextFactory;
        private HttpContextBase _httpContext = null;
        private readonly IRepository<Person> _personRepo;
        private readonly IRepository<Login> _loginRepo;
        private int? _loginId = null;
        private string _userName;
        private string _fullName;

        #endregion

        #region Constants

        private const string UserStateSessionKey = "MarUserState";
        private const string DefaultLanguage = "en-NZ";
        private const string DefaultCulture = "en-NZ";
        private const string DefaultTimeZoneIdentifier = "New Zealand Standard Time";

        #endregion

        public ApplicationContext(
            IHttpContextFactory httpContextFactory,
            IRepository<Person> personRepo,
            IRepository<Login> loginRepo)
        {
            _personRepo = personRepo;
            _loginRepo = loginRepo;
            // Using a guard clause ensures that if the DI container fails
            // to provide the dependency you will get an exception
            if (httpContextFactory == null) throw new ArgumentNullException("httpContextFactory");
            _httpContextFactory = httpContextFactory;
        }

        // Singleton pattern to access HTTP context at the right time
        private HttpContextBase HttpContext
        {
            get
            {
                if (this._httpContext == null)
                {
                    this._httpContext = _httpContextFactory.Create();
                }
                return this._httpContext;
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return HttpContext.User != null && HttpContext.User.Identity.IsAuthenticated;
            }
        }

        public void SignIn(string username)
        {
            if (HttpContext != null && HttpContext.Session != null)
            {
                HttpContext.Session.Abandon();
            }

            var user = _loginRepo.Get(n => n.Username == username).FirstOrDefault();
            if (user == null)
            {
                throw new Exception("User is not found on server");
            }
            //TODO: Audit method requires description parameter
            //_auditService.Audit(AuditActivityType.UserLogin, "User Login", null, user, user.Org, true);

            FormsAuthentication.SetAuthCookie(user.Username, false);
            UserState = InitializeUserState(user.Username);
        }

        //    /// <summary>
        //    /// Get login Id
        //    /// </summary>
        public int LoginId
        {
            get
            {
                var user = HttpContext.User;
                if (this._loginId == null && user != null && user.Identity.IsAuthenticated)
                {
                    var id = _loginRepo.GetQueryable().Where(x => x.Username.Equals(user.Identity.Name)).Select(x => x.Id).FirstOrDefault();
                    this._loginId = id;
                }
                return this._loginId.GetValueOrDefault();
            }
            set { this._loginId = value; }
        }

        public void ChangeLanguage(string language)
        {
            CurrentUser.Language = language;
            // TODO Update user
        }

        public void ChangeCulture(string culture)
        {
            CurrentUser.Culture = culture;
            // TODO Update user
        }

        public IUserState CurrentUser
        {
            get { return UserState ?? InitializeUserState(_httpContext.User.Identity.Name); }
        }

        private IUserState UserState
        {
            get
            {
                if (HttpContext.Session == null)
                {
                    throw new Exception("The _httpContext.Session is null.");
                }
                return HttpContext.Session[UserStateSessionKey] as IUserState;
            }
            set
            {
                if (HttpContext.Session == null)
                {
                    throw new Exception("The _httpContext.Session is null.");
                }
                HttpContext.Session[UserStateSessionKey] = value;
            }
        }

        public void SessionAbandon()
        {
            if (HttpContext != null && HttpContext.Session != null)
            {
                HttpContext.Session.Abandon();
            }
        }

        private IUserState InitializeUserState(Person person)
        {
            var userState = new UserState
            {
                Id = person.Id,
                FirstName = person.FirstName
            };
            return userState;
        }

        private IUserState InitializeUserState(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                SessionAbandon();
                return null;
            }
            var user = _loginRepo.GetQueryable().Where(n => n.Username == username).FirstOrDefault();
            if (user == null) return null;
            return new UserState
            {
                Id = user.Id,
                FirstName = user.Person.FirstName
            };
        }

        //    /// <summary>
        //    /// Get login username
        //    /// </summary>
        public string UserName
        {
            get
            {
                var user = HttpContext.User;

                if (this._userName == null && user != null && user.Identity.IsAuthenticated)
                {
                    this._userName = user.Identity.Name;
                }

                return this._userName;
            }
            set { this._userName = value; }
        }

        //    /// <summary>
        //    /// Get login username
        //    /// </summary>
        public string FullName
        {
            get
            {
                var user = HttpContext.User;

                if (this._fullName == null)
                {
                    if (user != null && user.Identity.IsAuthenticated)
                    {
                        var login = _loginRepo.GetQueryable().FirstOrDefault(x => x.Username.Equals(user.Identity.Name));
                        if (login != null && login.Person != null)
                        {
                            this._fullName = string.Format("{0} {1}", login.Person.FirstName, login.Person.LastName);
                        }
                    }
                }

                return this._fullName;
            }
            set { this._fullName = value; }
        }
    }
}
