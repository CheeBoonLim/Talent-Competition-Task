using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Talent.Service.Validation.ModelValidation
{
    public interface IModelValidationService<TCreateModel, TEditModel>
    {
        IEnumerable<ValidationResult> ValidateCreateModel(TCreateModel createModel);

        IEnumerable<ValidationResult> ValidateEditModel(TEditModel editModel);
    }
}
