using Talent.Data.Entities;
using Talent.Service.Models;

namespace Talent.Service.SignUp
{
    public interface ISignUpService
    {
        void Register(SignUpPersonal user);
        bool VerifyPassword(string email, string password);
        void ResetPassword(Login user, string newPassword);
    }
}
