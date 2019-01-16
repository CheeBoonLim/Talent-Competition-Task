using Talent.Data;
using Talent.Data.Entities;
using Talent.Service.Models;
using Talent.Service.Security;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace Talent.Service.SignUp
{
    public class SignUpService : ISignUpService
    {

        // These constants may be changed without breaking existing hashes.
        public const int SALT_BYTES = 24;
        public const int HASH_BYTES = 18;
        public const int PBKDF2_ITERATIONS = 64000;

        private IRepository<Person> _personRepository;
        private IRepository<Login> _loginRepository;
        private IPasswordStorage _encryptPassword;

        public SignUpService(IRepository<Person> personRepository,
                                IRepository<Login> loginRepository,
                                IPasswordStorage encryptPassword)
        {
            _personRepository = personRepository;
            _loginRepository = loginRepository;
            _encryptPassword = encryptPassword;
        }
        /// <summary>
        /// Register new customer
        /// </summary>
        /// <param name="user"></param>
        public void Register(SignUpPersonal user)
        {
            try
            {
                if (user == null) throw new ApplicationException("Incomplete register request - user is null");
                if (user.EmailAddress == null) throw new ApplicationException("Incomplete register request - user's email is null");
                if (user.Password == null || user.Password.Length == 0) throw new ApplicationException("Incomplete register request - Password is null");

                if (_loginRepository.GetQueryable().Any(n => n.Username == user.EmailAddress))
                {
                    throw new ApplicationException("Email address has been used in registration.");
                }

                // hash password
                var passHash = _encryptPassword.CreateHash(user.Password);

                //var passHash = new PBKDF2(user.Password,SALT_BYTES,PBKDF2_ITERATIONS,"HMACSHA512");

                var login = new Login()
                {
                    Username = user.EmailAddress,
                    PasswordHash = passHash,
                    IsDisabled = false,
                    EmailAddressAuthorized = false,
                    EmailCode = user.EmailCode,
                    ExpiredOn = DateTime.UtcNow.AddHours(24),
                    PasswordFormat = PBKDF2_ITERATIONS,
                    TermsAccepted = user.TermsConditionsAccepted
                };

                var person = new Person()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    GenderId = 1,
                    MobilePhone = user.MobileNumber,
                    CountryCode = user.CountryCode,
                    DialCode = user.DialCode,
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    UID = Guid.NewGuid(),
                    Login = login,
                };

                _personRepository.Add(person);

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Register error - " + ex.Message);
            }
        }

        /// <summary>
        /// Verify password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool VerifyPassword(string email, string password)
        {
            //TODO for testing purpose
            return true;
            //if (string.IsNullOrEmpty(password)) throw new ApplicationException("Login fail - Password is null");

            //var user = _loginRepository.GetQueryable().Where(x => x.Username.Equals(email)).FirstOrDefault();
            //if (user == null) throw new ApplicationException("Login fail - user is null");

            //return _encryptPassword.VerifyPassword(password, user.PasswordHash);
        }

        public void ResetPassword(Login user, string newPassword)
        {
            if (user == null) throw new ApplicationException("Incomplete reset password - user is null");
            // hash password
            var passHash = _encryptPassword.CreateHash(newPassword);
            user.PasswordHash = passHash;

            _loginRepository.Update(user);

        }
    }
}
