using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Talent.Service.Utilities
{
    public static class ModelStateExtensions
    {
        /// <summary>
        /// Adds a collection of ValidationResult erros to the model state error collection.
        /// </summary>
        /// <param name="modelState">ModelStateDictionary the errors are to be added to.</param>
        /// <param name="errors">Collection of ValidationResult errors to add to the ModelStateDictionary.</param>
        public static void AddModelErrors(this ModelStateDictionary modelState, IEnumerable<ValidationResult> errors)
        {
            foreach (var error in errors)
            {
                if (error.MemberNames.Any())
                {
                    foreach (var name in error.MemberNames)
                    {
                        modelState.AddModelError(name, error.ErrorMessage);
                    }
                }
                else
                {
                    modelState.AddModelError(string.Empty, error.ErrorMessage);
                }
            }
        }

        /// <summary> 
        /// Output the properties which are causing the issues when the model is binding. 
        /// </summary>
        public static void OutputErrors(this ModelStateDictionary modelState)
        {
#if DEBUG
            // Make sure nothing goes wrong.
            try
            {
                var errors = modelState.Where(a => a.Value.Errors.Count > 0)
                    .Select(b => new { b.Key, b.Value.Errors })
                    .ToArray();

                foreach (var modelStateErrors in errors)
                {
                    System.Diagnostics.Debug.WriteLine("!Binding Error!", modelStateErrors.Key);
                }
            }
            catch
            {
            }
#endif
        }
    }
}
