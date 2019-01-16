using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Talent.Service.Validation
{
    public class ValidationResultList<T> : List<ValidationResult>
    {
        public bool IsValid
        {
            get { return Count == 0; }
        }

        //public void Add(string validationMessage, params Expression<Func<T, object>>[] properties)
        //{
        //    Add(BuildValidationResult(validationMessage, properties));
        //}

        public void Add(string validationMessage)
        {
            Add(new ValidationResult(validationMessage, new[] { string.Empty }));
        }

        public void Add(string validationMessage, string propertyName)
        {
            Add(new ValidationResult(validationMessage, new[] { propertyName }));
        }

        //private static ValidationResult BuildValidationResult(string message, params Expression<Func<T, object>>[] properties)
        //{
        //    return new ValidationResult(message, properties.Select(ReflectionUtils.GetPropertyName));
        //}
    }
}
