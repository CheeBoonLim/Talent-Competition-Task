using Talent.Data.Entities;
using Talent.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talent.Service.Models;
using Talent.Service.Domain;

namespace Talent.Service.Persons
{
    public interface IPersonService
    {
        List<Person> GetList();
        void UpdateProfile(UserProfileEditModel model);
        Verification GetVerification(int id);
        void UpdateDocumentStatus(int id, DocumentStatus status, int personId, int updatedBy);
        void UpdateMobileCode(int id, string mobileCode);
    }
}
