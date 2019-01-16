using System;
using System.Web.Mvc;

namespace Talent.WebApp.Attributes
{
    /// <summary>
    /// Use in conjunction with a non-WriteRequired PatientAuthorizeAttribute on a Controller. Apply this attribute to an Action of that Controller to specify that the
    /// action requires the same permission as the controller, but requires that the user is allowed to make changes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class AuthorizationRequireWriteAttribute : FilterAttribute
    {
    }
}