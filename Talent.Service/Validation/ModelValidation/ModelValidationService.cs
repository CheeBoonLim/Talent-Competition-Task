using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Talent.Service.Validation.ModelValidation
{
    public class ModelValidationService<TCreateModel, TEditModel> : IModelValidationService<TCreateModel, TEditModel>
    {
        public virtual IEnumerable<ValidationResult> ValidateCreateModel(TCreateModel createModel)
        {
            return ValidateObject(createModel);
        }

        public virtual IEnumerable<ValidationResult> ValidateEditModel(TEditModel editModel)
        {
            return ValidateObject(editModel);
        }

        public virtual IEnumerable<ValidationResult> ValidateObject(object model)
        {
            var result = new List<ValidationResult>();

            return result;
        }
    }
}
